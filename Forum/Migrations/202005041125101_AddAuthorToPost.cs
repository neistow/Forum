namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "AuthorId");
            AddForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropColumn("dbo.Posts", "AuthorId");
        }
    }
}
