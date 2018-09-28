using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BeInPink.Models.Client;

namespace BeInPink.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        // [Index(IsUnique = true)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public enum _UserType
        {
            [Display(Name ="Someone who wants to give fitness services")]
            Coach =1,
            [Display(Name = "Someone who is seeking fitness services")]
            Client =2
        }
        [Display(Name ="Who are you?")]
        public _UserType WhoAreYou { get; set; }
    }

    public class ExternalLoginListViewModel
    {

        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RegisterCoachViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        // [Index(IsUnique = true)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]  
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Qualification")]
        [StringLength(256, ErrorMessage = "The {0} must not be larger than {1} charaters.")]
        public string Qualification { get; set; }

        [Display(Name = "Your brief description")]
        [StringLength(1000, ErrorMessage = "The {0} must not be larger than {1} charaters.")]
        public string Description { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must not be larger than {1} charaters.")]
        [Display(Name = "What coaching would you provide?")]
        public string CoachingType { get; set; }

        [Display(Name = "Coaching Specialization")]
        [StringLength(50, ErrorMessage = "The {0} must not be larger than {1} charaters.")]
        public string Specialization { get; set; }

        [Display(Name = "Where are you located?")]
        [StringLength(25, ErrorMessage = "The {0} must not be larger than {1} charaters.")]
        public string WorkLocation { get; set; }
    }
    public class RegisterClientViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DateLessThanToday(ErrorMessage ="Do you come from future?")]
        public DateTime DOB { get; set; }
        [Required]
        public _Gender Sex { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string   ConfirmPassword { get; set; }

        [Range(20, 280, ErrorMessage = "This height is not human")]
        [Display(Name = "Height in CMs")]
        public double? Height { get; set; }
        [Range(2, 650, ErrorMessage = "This weight is not human")]
        [Display(Name = "Weight in KGs")]
        public double? Weight { get; set; }
        [Range(2, 650, ErrorMessage = "This weight is not human")]
        [Display(Name = "What is your target weight in KGs")]
        public double? TargetWeight { get; set; }
        [Display(Name ="How is your lifestyle?")]
        public _ActivityFactor? LifeStyle { get; set; }
        [Display(Name = "What are you here for?")]
        public _FitnessPlan? FitnessPlan { get; set; }
    }
}
