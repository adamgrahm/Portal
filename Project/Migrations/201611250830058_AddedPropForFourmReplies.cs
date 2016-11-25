namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropForFourmReplies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reply = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ThreadPosts", "Replies_Id", c => c.Int());
            CreateIndex("dbo.ThreadPosts", "Replies_Id");
            AddForeignKey("dbo.ThreadPosts", "Replies_Id", "dbo.ForumReplies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadPosts", "Replies_Id", "dbo.ForumReplies");
            DropIndex("dbo.ThreadPosts", new[] { "Replies_Id" });
            DropColumn("dbo.ThreadPosts", "Replies_Id");
            DropTable("dbo.ForumReplies");
        }
    }
}
