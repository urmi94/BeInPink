using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime BookingDateTime { get; set; }
        
        public String BookingClientId { get; set; }
        [ForeignKey("BookingClientId")]
        [InverseProperty("BookingClients")]
        public virtual Client Client { get; set; }
        
        public string BookingCoachId { get; set; }
        [ForeignKey("BookingCoachId")]
        [InverseProperty("BookingCoachs")]
        public virtual Coach Coach { get; set; }

        public enum _bookingStatus { Requested = 1, Confirmed = 2, Rejected = 3 }
        public _bookingStatus BookingStatus { get; set; } = _bookingStatus.Requested;
        [Display(Name = "Rejection Reason")]
        public String RejectReason { get; set; }
    }
}