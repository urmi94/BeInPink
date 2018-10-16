namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBookingModel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BookingViewModels");
            AlterColumn("dbo.Bookings", "BookingClientId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Bookings", "BookingCoachId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.BookingViewModels", new[] { "BookingDateTime", "BookingCoach" });
            CreateIndex("dbo.Bookings", "BookingClientId");
            CreateIndex("dbo.Bookings", "BookingCoachId");
            AddForeignKey("dbo.Bookings", "BookingClientId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bookings", "BookingCoachId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BookingViewModels", "BookingClient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookingViewModels", "BookingClient", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Bookings", "BookingCoachId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "BookingClientId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "BookingCoachId" });
            DropIndex("dbo.Bookings", new[] { "BookingClientId" });
            DropPrimaryKey("dbo.BookingViewModels");
            AlterColumn("dbo.Bookings", "BookingCoachId", c => c.String());
            AlterColumn("dbo.Bookings", "BookingClientId", c => c.String());
            AddPrimaryKey("dbo.BookingViewModels", new[] { "BookingDateTime", "BookingClient", "BookingCoach" });
        }
    }
}
