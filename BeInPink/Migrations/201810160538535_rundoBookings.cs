namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rundoBookings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "BookingClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "BookingCoachId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "BookingClientId" });
            DropIndex("dbo.Bookings", new[] { "BookingCoachId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookingViewModels",
                c => new
                    {
                        BookingDateTime = c.DateTime(nullable: false),
                        BookingCoach = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BookingDateTime, t.BookingCoach });
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingDateTime = c.DateTime(nullable: false),
                        BookingClientId = c.String(maxLength: 128),
                        BookingCoachId = c.String(maxLength: 128),
                        BookingStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID);
            
            CreateIndex("dbo.Bookings", "BookingCoachId");
            CreateIndex("dbo.Bookings", "BookingClientId");
            AddForeignKey("dbo.Bookings", "BookingCoachId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bookings", "BookingClientId", "dbo.AspNetUsers", "Id");
        }
    }
}
