namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SendEmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendEmailViewModels",
                c => new
                    {
                        ToEmail = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false),
                        Contents = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ToEmail);
            
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(maxLength: 250));
            DropTable("dbo.SendEmailViewModels");
        }
    }
}
