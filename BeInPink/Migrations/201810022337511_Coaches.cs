namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coaches : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ViewCoachesViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ViewCoachesViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        Qualification = c.String(),
                        Description = c.String(),
                        CoachingType = c.String(),
                        Specialization = c.String(),
                        WorkLocation = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
    }
}
