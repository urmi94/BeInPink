using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeInPink.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string BookingClientId { get; set; }
        public string BookingCoachId { get; set; }
        public enum _bookingStatus { Requested = 1, Confirmed = 2, Rejected = 3 }
        public _bookingStatus BookingStatus { get; set; } = _bookingStatus.Requested;
    }

    public class BookingViewModel
    {
        [Key]
        [Column(Order = 1)]
        public DateTime BookingDateTime { get; set; }
        [Key]
        [Column(Order = 2)]
        public String BookingClient { get; set; }
        [Key]
        [Column(Order = 3)]
        public String BookingCoach { get; set; }

    }
}