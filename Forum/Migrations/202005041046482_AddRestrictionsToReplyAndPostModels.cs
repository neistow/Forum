namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestrictionsToReplyAndPostModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Posts", "Text", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("dbo.Replies", "Text", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Replies", "Text", c => c.String());
            AlterColumn("dbo.Posts", "Text", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
