namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterCoach : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterCoachViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        Qualification = c.String(),
                        Description = c.String(maxLength: 1000),
                        CoachingType = c.String(nullable: false),
                        Specialization = c.String(),
                        WorkLocation = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterCoachViewModels");
        }
    }
}
