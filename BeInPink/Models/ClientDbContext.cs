using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<User> users { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.Client> Clients { get; set; }
        //    public DbSet<Client> clients { get; set; }
    }
}
