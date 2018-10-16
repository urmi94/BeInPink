namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPostUpdatedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PostUpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "PostUpdatedOn", c => c.DateTime(nullable: false));
        }
    }
}
