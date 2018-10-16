namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newBookingsWithoutViewModel : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.AspNetUsers", t => t.BookingClientId)
                .ForeignKey("dbo.AspNetUsers", t => t.BookingCoachId)
                .Index(t => t.BookingClientId)
                .Index(t => t.BookingCoachId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "BookingCoachId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "BookingClientId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "BookingCoachId" });
            DropIndex("dbo.Bookings", new[] { "BookingClientId" });
            DropTable("dbo.Bookings");
        }
    }
}
