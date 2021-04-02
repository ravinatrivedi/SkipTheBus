using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideshareIdentityFramework.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string ApplicationUserID { get; set; }
        public int RideID { get; set; }
        public DateTime BookedTime { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; }
        public decimal Refund { get; set; }
        public virtual Ride Ride { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}