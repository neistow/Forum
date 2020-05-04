namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorToReply : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            AddColumn("dbo.Replies", "AuthorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "AuthorId");
            CreateIndex("dbo.Replies", "AuthorId");
            AddForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Replies", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(maxLength: 128));
            DropColumn("dbo.Replies", "AuthorId");
            CreateIndex("dbo.Posts", "AuthorId");
            AddForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
