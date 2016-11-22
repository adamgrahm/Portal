namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbSetNewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Headline = c.String(),
                        Content = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        SentFrom_Id = c.String(maxLength: 128),
                        SentTo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SentFrom_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SentTo_Id)
                .Index(t => t.SentFrom_Id)
                .Index(t => t.SentTo_Id);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Headline = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        OriginalPoster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OriginalPoster_Id)
                .Index(t => t.OriginalPoster_Id);
            
            CreateTable(
                "dbo.ThreadPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForumPost = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        OriginalPoster_Id = c.String(maxLength: 128),
                        Thread_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OriginalPoster_Id)
                .ForeignKey("dbo.Threads", t => t.Thread_Id)
                .Index(t => t.OriginalPoster_Id)
                .Index(t => t.Thread_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadPosts", "Thread_Id", "dbo.Threads");
            DropForeignKey("dbo.ThreadPosts", "OriginalPoster_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Threads", "OriginalPoster_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SentTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SentFrom_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ThreadPosts", new[] { "Thread_Id" });
            DropIndex("dbo.ThreadPosts", new[] { "OriginalPoster_Id" });
            DropIndex("dbo.Threads", new[] { "OriginalPoster_Id" });
            DropIndex("dbo.Messages", new[] { "SentTo_Id" });
            DropIndex("dbo.Messages", new[] { "SentFrom_Id" });
            DropTable("dbo.ThreadPosts");
            DropTable("dbo.Threads");
            DropTable("dbo.Messages");
        }
    }
}
