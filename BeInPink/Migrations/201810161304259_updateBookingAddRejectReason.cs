namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateBookingAddRejectReason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "RejectReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "RejectReason");
        }
    }
}
