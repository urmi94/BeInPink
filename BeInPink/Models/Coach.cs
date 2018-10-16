using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class Coach: ApplicationUser
    {
        public virtual ICollection<Booking> BookingCoachs { get; set; }
        public string CoachingType { get; set; }
        public string Description { get; set; }
        public string WorkLocation { get; set; }
       
        public string Qualification { get; set; }
        public string Specialization { get; set; }
    }

    public class Admin : ApplicationUser
    {
        
    }
}