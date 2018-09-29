using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //Coach
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
    public class EditClientProfileViewModel
    {
        [Key]
        [Display(Name = "Email")]
        public string Email { get; set; }
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

        //Client
        [Required]
        public _Gender Sex { get; set; }
        [Range(20, 280, ErrorMessage = "This height is not human")]
        [Display(Name = "Height in CMs")]
        public double? Height { get; set; }
        [Range(2, 650, ErrorMessage = "This weight is not human")]
        [Display(Name = "Weight in KGs")]
        public double? Weight { get; set; }
        [Range(2, 650, ErrorMessage = "This weight is not human")]
        [Display(Name = "What is your target weight in KGs")]
        public double? TargetWeight { get; set; }
        [Display(Name ="When do you want to reach your target weight?")]
        public DateTime? TargetDate { get; set; }
        [Display(Name = "How is your lifestyle?")]
        public _ActivityFactor? LifeStyle { get; set; }
        [Display(Name = "What are you here for?")]
        public _FitnessPlan? FitnessPlan { get; set; }

        public double? Waist { get; set; }
        public double? Hip { get; set; }
        
       
        [Display(Name = "Your Waist/Hip Ratio")]
        public double? WaistToHipRatio
        {
            get
            {
                return Waist / Hip;
            }
            set
            { }
        }
        public double? BMI
        {
            get
            {
                return (Weight / Height / Height) * 10000;
            }
            set { }
        }
        [Display(Name = "What your BMI says about you")]
        public string BmiResult
        {
            get
            {
                if (BMI < 18.5)
                    return "Underweight";
                if (BMI >= 18.5 && BMI < 25)
                    return "Healthy";
                if (BMI >= 25 && BMI < 30)
                    return "Overweight";
                if (BMI >= 30)
                    return "Obese";
                else
                    return "";
            }
            set { }
        }
        public double? BMR
        {
            get
            {
                if (Sex == _Gender.Female)
                    return (655 + (4.35 * Weight * 2.20462) + (4.7 * Height * 0.393701) - (4.7 * Age));
                else
                    return (66 + (6.23 * Weight * 2.20462) + (12.7 * Height * 0.393701) - (6.8 * Age));
            }
            set { }
        }
        public double Age
        {
            get
            {
                return DateTime.Today.Year - DOB.Year;
            }
            set { }
        }
        
        [Display(Name = "Let the coach know about your food preferences")]
        public _CusinePreference? CusinePreference { get; set; }
        
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Display(Name = "Your Body Water Percentage")]
        public double? BodyWaterPercentage
        {
            get
            {
                if (Sex == _Gender.Female)
                    return (-2.097 + (0.1069 * Height) + (0.2466 * Weight));
                else
                    return (2.447 - (0.09156 * Age) + (0.1074 * Height) + (0.3362 * Weight));
            }
            set { }
        }
        [Display(Name = "Your Body Fat Percentage")]
        public double? BodyFatPercentage
        {
            get
            {
                if (Sex == _Gender.Female)
                    return ((Weight - (((((Weight * 0.732) + 9.987 + (5 / 3.14)) - (Waist * 0.157)) - (Hip * 0.249)) + (8 * 0.434))) / Weight) * 100;
                else
                    return ((Weight - (((Weight * 1.082) + 94.42) - (Waist * 4.15))) / Weight) * 100;
            }
            set { }
        }
        public double? Heamoglobin { get; set; }
        [Display(Name = "Systole Blood Pressure")]
        public int? SystoleBP { get; set; }
        [Display(Name = "Diastole Blood Pressure")]
        public int? DiastoleBP { get; set; }
        [Display(Name = "High-Density Lipoproteins Count")]
        public double? HDL { get; set; }
        [Display(Name = "Thyroid Stimulating Hormone Level")]
        public double? TSH { get; set; }
        [Display(Name = "Random Blood Sugar Level")]
        public double? RandomSugar { get; set; }
        [Display(Name = "Fasting Blood Sugar Level")]
        public double? FastingSugar { get; set; }
        [Display(Name = "Post Prandial Blood Sugar Level")]
        public double? PostPrandialSugar { get; set; }
        [Display(Name = "Any Medical Condition?")]
        public string MedicalCondition { get; set; }
        [Display(Name = "Daily Calorie Intake")]
        public double? DailyCalorieIntake
        {
            get
            {
                if (LifeStyle.ToString() == "Sedentary")
                    return BMR * 1.2;
                if (LifeStyle.ToString() == "LightlyActive")
                    return BMR * 1.375;
                if (LifeStyle.ToString() == "ModeratelyActive")
                    return BMR * 1.55;
                if (LifeStyle.ToString() == "VeryActive")
                    return BMR * 1.725;
                if (LifeStyle.ToString() == "ExtremelyActive")
                    return BMR * 1.9;
                else
                    return null;
            }
            set { }
        }
        [Display(Name = "Daily Carbohydrates Intake")]
        public double? DailyCarbIntake { get; set; }
        [Display(Name = "Daily Protien Intake")]
        public double? DailyProteinIntake { get; set; }
        [Display(Name = "Daily Fat Intake")]
        public double? DailyFatIntake { get; set; }
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
