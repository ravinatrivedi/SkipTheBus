using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RideshareIdentityFramework.Models
{
    public class Ride
    {
        public int RideID { get; set; }
        public string ApplicationUserID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "StartLocation")]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "EndLocation")]
        public string EndLocation { get; set; }

        [Required]
        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "PickUpLocation")]
        public string PickUpLocation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "DropLocation")]
        public string DropLocation { get; set; }

        [Required]
        [Range(0,4)]
        [Display(Name = "SeatAvailable")]
        public int SeatRemaining { get; set; }

        [Required]
        [Range(10,30)]
        [Display(Name = "FarePerSeat")]
        public decimal FarePerSeat { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}