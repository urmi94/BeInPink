namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditClientProfile3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DailyCalorieIntake", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DailyCalorieIntake", c => c.Double());
        }
    }
}
