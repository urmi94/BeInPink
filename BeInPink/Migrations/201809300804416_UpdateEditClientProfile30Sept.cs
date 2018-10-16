namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEditClientProfile30Sept : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EditClientProfileViewModels", "Age", c => c.Int());
            AlterColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EditClientProfileViewModels", "DailyCalorieIntake", c => c.Double());
            AlterColumn("dbo.EditClientProfileViewModels", "Age", c => c.Double(nullable: false));
        }
    }
}
