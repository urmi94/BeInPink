namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCoachProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditCoachProfileViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Qualification = c.String(maxLength: 256),
                        Description = c.String(maxLength: 1000),
                        CoachingType = c.String(nullable: false, maxLength: 250),
                        Specialization = c.String(maxLength: 50),
                        WorkLocation = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EditCoachProfileViewModels");
        }
    }
}
