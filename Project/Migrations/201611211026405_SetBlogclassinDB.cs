namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetBlogclassinDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Post = c.String(),
                        Blog_Id = c.Int(),
                        WhoPosted_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.WhoPosted_Id)
                .Index(t => t.Blog_Id)
                .Index(t => t.WhoPosted_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "WhoPosted_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blogs", "Blog_Id", "dbo.Blogs");
            DropIndex("dbo.Blogs", new[] { "WhoPosted_Id" });
            DropIndex("dbo.Blogs", new[] { "Blog_Id" });
            DropTable("dbo.Blogs");
        }
    }
}
