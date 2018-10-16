namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingDateTime = c.DateTime(nullable: false),
                        BookingClientId = c.String(),
                        BookingCoachId = c.String(),
                        BookingStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID);
            
            CreateTable(
                "dbo.BookingViewModels",
                c => new
                    {
                        BookingDateTime = c.DateTime(nullable: false),
                        BookingClient = c.String(nullable: false, maxLength: 128),
                        BookingCoach = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BookingDateTime, t.BookingClient, t.BookingCoach });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookingViewModels");
            DropTable("dbo.Bookings");
        }
    }
}
