using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeInPink.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<Post> PostAuthor { get; set; }
        public virtual ICollection<Post> PostUpdatedByAuthor { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<BeInPink.Models.RegisterClientViewModel> Clients { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.RegisterCoachViewModel> Coaches { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.EditClientProfileViewModel> EditClientProfileViewModels { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.EditCoachProfileViewModel> EditCoachProfileViewModels { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.SendEmailViewModel> SendEmailViewModels { get; set; }


        public System.Data.Entity.DbSet<BeInPink.Models.Coach> CoachUsers { get; set; }
        public System.Data.Entity.DbSet<BeInPink.Models.Client> ClientUsers { get; set; }
        public System.Data.Entity.DbSet<BeInPink.Models.Admin> Admins { get; set; }
        public System.Data.Entity.DbSet<BeInPink.Models.Booking> Bookings { get; set; }

        public System.Data.Entity.DbSet<BeInPink.Models.Post> Posts { get; set; }



        //public System.Data.Entity.DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }

   
}
