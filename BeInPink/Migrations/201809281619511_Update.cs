namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterCoachViewModels", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterCoachViewModels", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterCoachViewModels", "Qualification", c => c.String(maxLength: 256));
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.RegisterCoachViewModels", "Specialization", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterCoachViewModels", "WorkLocation", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterCoachViewModels", "WorkLocation", c => c.String());
            AlterColumn("dbo.RegisterCoachViewModels", "Specialization", c => c.String());
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterCoachViewModels", "Qualification", c => c.String());
            AlterColumn("dbo.RegisterCoachViewModels", "LastName", c => c.String());
            AlterColumn("dbo.RegisterCoachViewModels", "FirstName", c => c.String());
        }
    }
}
