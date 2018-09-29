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
        [DateLessThanToday]
        public DateTime DOB { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
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
        //public System.Data.Entity.DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }

    public class DateLessThanTodayAttribute : ValidationAttribute
    {
        private DateTime _MaxDate;


        public DateLessThanTodayAttribute()
        {
            _MaxDate = DateTime.Today.AddDays(-1);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
            if (dt < DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Do you come from future?");

        }
    }
}
