using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class Client : ApplicationUser
    {
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? TargetWeight { get; set; }
        public DateTime? TargetDate { get; set; }
        public double? Waist { get; set; }
        public double? Hip { get; set; }
        public double? WaistToHipRatio
        {
            get
            {
                return Waist / Hip;
            }
        }
        public double? BMI
        {
            get
            {
                return (Weight / Height / Height) * 10000;
            }
        }
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
        }
        public enum _Gender
        {
            Male = 1,
            Female = 2,
            Others = 3
        }
        public _Gender Sex { get; set; }
        public double Age
        {
            get
            {
                return DateTime.Today.Year - DOB.Year;
            }
        }
        public enum _ActivityFactor
        {
            Sedentary=1,
            [Display(Name = "Lightly Active")]
            LightlyActive=2,
            [Display(Name = "Moderately Active")]
            ModeratelyActive=3,
            [Display(Name = "Very Active")]
            VeryActive=4,
            [Display(Name = "Extremely Active")]
            ExtremelyActive=5
        }
        public _ActivityFactor? LifeStyle { get; set; }

        public enum _CusinePreference
        {
            [Display(Name = "Vegetarian")]
            Veg = 1,
            [Display(Name = "Non-Vegetarian")]
            NonVeg = 2,

            Vegan = 3
        }
        public _CusinePreference? CusinePreference { get; set; }
        public string BloodGroup { get; set; }
        public double? BodyWaterPercentage
        {
            get
            {
                if (Sex == _Gender.Female)
                    return (-2.097 + (0.1069 * Height) + (0.2466 * Weight));
                else
                    return (2.447 - (0.09156 * Age) + (0.1074 * Height) + (0.3362 * Weight));
            }
        }
        public double? BodyFatPercentage
        {
            get
            {
                if (Sex == _Gender.Female)
                    return ((Weight - (((((Weight * 0.732) + 9.987 + (5 / 3.14)) - (Waist * 0.157)) - (Hip * 0.249)) + (8 * 0.434))) / Weight) * 100;
                else
                    return ((Weight - (((Weight * 1.082) + 94.42) - (Waist * 4.15))) / Weight) * 100;
            }
        }
        public double? Heamoglobin { get; set; }
        public int? SystoleBP { get; set; }
        public int? DiastoleBP { get; set; }
        public double? HDL { get; set; }
        public double? TSH { get; set; }
        public double? RandomSugar { get; set; }
        public double? FastingSugar { get; set; }
        public double? PostPrandialSugar { get; set; }
        public string MedicalCondition { get; set; }
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
        }
        public double? DailyCarbIntake { get; set; }
        public double? DailyProteinIntake { get; set; }
        public double? DailyFatIntake { get; set; }
        public enum _FitnessPlan
        {
            [Display(Name = "Weight Loss")]
            WeightLoss = 1,
            [Display(Name = "Weight Gain")]
            WeighGain = 2,
            [Display(Name = "Muscle Gain")]
            MuscleGain = 3,
            [Display(Name = "Muscle Loss")]
            MuscleLoss = 4,
            [Display(Name = "Body Toning")]
            BodyToning = 5,
            [Display(Name = "Stay Fit")]
            StayFit = 6
        }
        public _FitnessPlan? FitnessPlan { get; set; }
    }
}