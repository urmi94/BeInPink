namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMig29Sept : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterCoachViewModels", "CoachingType", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
