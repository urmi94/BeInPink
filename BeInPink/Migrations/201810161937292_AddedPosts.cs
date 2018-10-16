namespace BeInPink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostHeading = c.String(),
                        PostContent = c.String(),
                        PostType = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        PostUpdatedOn = c.DateTime(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                        UpdatedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.AuthorId)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "UpdatedById" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropTable("dbo.Posts");
        }
    }
}
