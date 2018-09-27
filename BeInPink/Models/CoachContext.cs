using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class CoachContext : DbContext
    {
        public CoachContext():base("DefaultConnection")
        {

        }
        public DbSet<User> users { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.Coach> Coaches { get; set; }
        // public DbSet<Coach> coaches { get; set; }
    }
}