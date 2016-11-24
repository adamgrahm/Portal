namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageAndPostedbyToTheBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "ImageURL", c => c.String());
            AddColumn("dbo.Blogs", "PostedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "PostedBy");
            DropColumn("dbo.Blogs", "ImageURL");
        }
    }
}
