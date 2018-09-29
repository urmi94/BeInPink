namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditClientProfile2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "BMI", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "BmiResult", c => c.String());
            AddColumn("dbo.EditClientProfileViewModels", "BMR", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "Age", c => c.Double(nullable: false));
            AddColumn("dbo.EditClientProfileViewModels", "BodyWaterPercentage", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "BodyFatPercentage", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake", c => c.Double());
            AddColumn("dbo.AspNetUsers", "BMR", c => c.Double());
            AddColumn("dbo.AspNetUsers", "Age", c => c.Double());
            AddColumn("dbo.AspNetUsers", "BodyWaterPercentage", c => c.Double());
            AddColumn("dbo.AspNetUsers", "BodyFatPercentage", c => c.Double());
            AddColumn("dbo.AspNetUsers", "DailyCalorieIntake", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DailyCalorieIntake");
            DropColumn("dbo.AspNetUsers", "BodyFatPercentage");
            DropColumn("dbo.AspNetUsers", "BodyWaterPercentage");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "BMR");
            DropColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake");
            DropColumn("dbo.EditClientProfileViewModels", "BodyFatPercentage");
            DropColumn("dbo.EditClientProfileViewModels", "BodyWaterPercentage");
            DropColumn("dbo.EditClientProfileViewModels", "Age");
            DropColumn("dbo.EditClientProfileViewModels", "BMR");
            DropColumn("dbo.EditClientProfileViewModels", "BmiResult");
            DropColumn("dbo.EditClientProfileViewModels", "BMI");
            DropColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio");
        }
    }
}
