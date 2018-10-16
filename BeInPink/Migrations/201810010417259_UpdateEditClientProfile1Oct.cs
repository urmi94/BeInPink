namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditClientProfile1Oct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio");
            DropColumn("dbo.EditClientProfileViewModels", "BMI");
            DropColumn("dbo.EditClientProfileViewModels", "BmiResult");
            DropColumn("dbo.EditClientProfileViewModels", "BMR");
            DropColumn("dbo.EditClientProfileViewModels", "Age");
            DropColumn("dbo.EditClientProfileViewModels", "BodyWaterPercentage");
            DropColumn("dbo.EditClientProfileViewModels", "BodyFatPercentage");
            DropColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake");
            DropColumn("dbo.EditClientProfileViewModels", "DailyCarbIntake");
            DropColumn("dbo.EditClientProfileViewModels", "DailyProteinIntake");
            DropColumn("dbo.EditClientProfileViewModels", "DailyFatIntake");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EditClientProfileViewModels", "DailyFatIntake", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "DailyProteinIntake", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "DailyCarbIntake", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake", c => c.Int());
            AddColumn("dbo.EditClientProfileViewModels", "BodyFatPercentage", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "BodyWaterPercentage", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "Age", c => c.Int());
            AddColumn("dbo.EditClientProfileViewModels", "BMR", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "BmiResult", c => c.String());
            AddColumn("dbo.EditClientProfileViewModels", "BMI", c => c.Double());
            AddColumn("dbo.EditClientProfileViewModels", "WaistToHipRatio", c => c.Double());
        }
    }
}
