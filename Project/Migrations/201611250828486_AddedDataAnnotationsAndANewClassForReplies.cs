namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotationsAndANewClassForReplies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThreadPosts", "Thread_Id", "dbo.Threads");
            DropIndex("dbo.ThreadPosts", new[] { "Thread_Id" });
            AlterColumn("dbo.Threads", "Headline", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Threads", "PostedBy", c => c.String(nullable: false));
            AlterColumn("dbo.ThreadPosts", "ForumPost", c => c.String(nullable: false));
            AlterColumn("dbo.ThreadPosts", "PostedBy", c => c.String(nullable: false));
            AlterColumn("dbo.ThreadPosts", "Thread_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ThreadPosts", "Thread_Id");
            AddForeignKey("dbo.ThreadPosts", "Thread_Id", "dbo.Threads", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadPosts", "Thread_Id", "dbo.Threads");
            DropIndex("dbo.ThreadPosts", new[] { "Thread_Id" });
            AlterColumn("dbo.ThreadPosts", "Thread_Id", c => c.Int());
            AlterColumn("dbo.ThreadPosts", "PostedBy", c => c.String());
            AlterColumn("dbo.ThreadPosts", "ForumPost", c => c.String());
            AlterColumn("dbo.Threads", "PostedBy", c => c.String());
            AlterColumn("dbo.Threads", "Headline", c => c.String());
            CreateIndex("dbo.ThreadPosts", "Thread_Id");
            AddForeignKey("dbo.ThreadPosts", "Thread_Id", "dbo.Threads", "Id");
        }
    }
}
