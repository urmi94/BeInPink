using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class User
    {
        [Key]
        public String username { get; set; }
        public String password { get; set; }
        public String userType { get; set; }

    }
}