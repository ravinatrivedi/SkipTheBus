using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using RideshareIdentityFramework.Models;

namespace RideshareIdentityFramework.Controllers
{
    public class RideController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ride
        public ActionResult Index()
        {
            return View(db.Rides.Where(r => r.SeatRemaining > 0 ).ToList());
        }

        // GET: Ride/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            string userName = User.Identity.Name;
            ViewBag.UserName = userName;
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // GET: Ride/
        //[Authorize(Roles = "Driver")]
        public ActionResult Create()
        {
            if (User.IsInRole("Driver")) {
                string userName = User.Identity.Name;
                ViewBag.UserName = userName;
                return View();
            }
            else
            {
                return View("Unauthorized");
            }
            
        }

        // POST: Ride/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartLocation,EndLocation,StartDate,EndDate,PickUpLocation,DropLocation,SeatRemaining,FarePerSeat")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                ride.ApplicationUserID = User.Identity.GetUserId();
                db.Rides.Add(ride);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = ride.RideID });
            }
            return View(ride);
        }

        // GET: Ride/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // POST: Ride/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RideID,ApplicationUserID,StartLocation,EndLocation,StartDate,EndDate,PickUpLocation,DropLocation,SeatRemaining,FarePerSeat")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ride).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ride);
        }

        // GET: Ride/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // POST: Ride/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ride ride = db.Rides.Find(id);
            db.Rides.Remove(ride);
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
