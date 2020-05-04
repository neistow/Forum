namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepliesToPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Replies", new[] { "AuthorId" });
            AddColumn("dbo.Replies", "Post_Id", c => c.Int());
            AlterColumn("dbo.Replies", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Replies", "AuthorId");
            CreateIndex("dbo.Replies", "Post_Id");
            AddForeignKey("dbo.Replies", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Replies", new[] { "Post_Id" });
            DropIndex("dbo.Replies", new[] { "AuthorId" });
            AlterColumn("dbo.Replies", "AuthorId", c => c.String(maxLength: 128));
            DropColumn("dbo.Replies", "Post_Id");
            CreateIndex("dbo.Replies", "AuthorId");
            AddForeignKey("dbo.Replies", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
