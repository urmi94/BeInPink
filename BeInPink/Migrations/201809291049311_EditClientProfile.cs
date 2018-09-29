namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditClientProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditClientProfileViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        Height = c.Double(),
                        Weight = c.Double(),
                        TargetWeight = c.Double(),
                        TargetDate = c.DateTime(),
                        LifeStyle = c.Int(),
                        FitnessPlan = c.Int(),
                        Waist = c.Double(),
                        Hip = c.Double(),
                        CusinePreference = c.Int(),
                        BloodGroup = c.String(),
                        Heamoglobin = c.Double(),
                        SystoleBP = c.Int(),
                        DiastoleBP = c.Int(),
                        HDL = c.Double(),
                        TSH = c.Double(),
                        RandomSugar = c.Double(),
                        FastingSugar = c.Double(),
                        PostPrandialSugar = c.Double(),
                        MedicalCondition = c.String(),
                        DailyCarbIntake = c.Double(),
                        DailyProteinIntake = c.Double(),
                        DailyFatIntake = c.Double(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EditClientProfileViewModels");
        }
    }
}
