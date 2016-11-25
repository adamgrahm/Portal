namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationBeetweenRepliesAndPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForumReplies", "Thread_Id", "dbo.Threads");
            DropForeignKey("dbo.ThreadPosts", "Replies_Id", "dbo.ForumReplies");
            DropIndex("dbo.ThreadPosts", new[] { "Replies_Id" });
            DropIndex("dbo.ForumReplies", new[] { "Thread_Id" });
            DropColumn("dbo.ForumReplies", "Id");
            RenameColumn(table: "dbo.ForumReplies", name: "Replies_Id", newName: "Id");
            DropPrimaryKey("dbo.ForumReplies");
            AlterColumn("dbo.ForumReplies", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ForumReplies", "Id");
            CreateIndex("dbo.ForumReplies", "Id");
            DropColumn("dbo.ThreadPosts", "Replies_Id");
            DropColumn("dbo.ForumReplies", "Thread_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForumReplies", "Thread_Id", c => c.Int());
            AddColumn("dbo.ThreadPosts", "Replies_Id", c => c.Int());
            DropIndex("dbo.ForumReplies", new[] { "Id" });
            DropPrimaryKey("dbo.ForumReplies");
            AlterColumn("dbo.ForumReplies", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ForumReplies", "Id");
            RenameColumn(table: "dbo.ForumReplies", name: "Id", newName: "Replies_Id");
            AddColumn("dbo.ForumReplies", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.ForumReplies", "Thread_Id");
            CreateIndex("dbo.ThreadPosts", "Replies_Id");
            AddForeignKey("dbo.ThreadPosts", "Replies_Id", "dbo.ForumReplies", "Id");
            AddForeignKey("dbo.ForumReplies", "Thread_Id", "dbo.Threads", "Id");
        }
    }
}
