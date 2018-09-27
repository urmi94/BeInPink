using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class Client : User
    {
        public int targetWeight { get; set; }
    }
}