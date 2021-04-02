using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RideshareIdentityFramework.Models;
using Transaction = RideshareIdentityFramework.Models.Transaction;

namespace RideshareIdentityFramework.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Ride);
            return View(bookings.ToList());
        }

        [Authorize]
        // GET: Booking/Details/5
        public ActionResult Details(int? id, Transaction transaction)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            //    [Bind(Include = "TransactionID,BookingID,ApplicationUserID,TransactionTime,AmountEarned,AmountSpent")]
            transaction.BookingID = booking.BookingID;
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            transaction.ApplicationUserID = userIdClaim.Value;
            transaction.TransactionTime = DateTime.Now;
            transaction.AmountSpent = booking.AmountPaid;
            db.Transactions.Add(transaction);
            db.SaveChanges();
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        [Authorize]
        // GET: Booking/Create
        public ActionResult Create(int? RideID, decimal AmountPaid)
        {
            ViewBag.RideID = RideID;
            ViewBag.AmountPaid = AmountPaid;
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,ApplicationUserID,RideID,BookedTime,AmountPaid,Status,Refund")] Booking booking)
        {
            if (ModelState.IsValid)
            {
               
                Ride ride = db.Rides.Find(booking.RideID);
                if (ride.ApplicationUserID == User.Identity.GetUserId())
                {
                    return View("Error");
                }
                else
                {
                    booking.ApplicationUserID = User.Identity.GetUserId(); 
                    booking.BookedTime = DateTime.Now;
                    booking.Status = "Confirmed";
                    booking.Refund = 0;
                    ride.SeatRemaining = ride.SeatRemaining - 1;
                    db.Entry(ride).State = EntityState.Modified;
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = booking.BookingID });
                }
            }

            ViewBag.RideID = new SelectList(db.Rides, "RideID", "ApplicationUserID", booking.RideID);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RideID = new SelectList(db.Rides, "RideID", "ApplicationUserID", booking.RideID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,ApplicationUserID,RideID,BookedTime,AmountPaid,Status,Refund")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RideID = new SelectList(db.Rides, "RideID", "ApplicationUserID", booking.RideID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
