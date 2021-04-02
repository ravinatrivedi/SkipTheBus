using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RideshareIdentityFramework.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int BookingID { get; set; }
        public string ApplicationUserID { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal AmountEarned { get; set; }
        public decimal AmountSpent { get; set; }
        public virtual Booking Booking { get; set; }
    }
}