namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOpToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Threads", "PostedBy", c => c.String());
            AddColumn("dbo.ThreadPosts", "PostedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThreadPosts", "PostedBy");
            DropColumn("dbo.Threads", "PostedBy");
        }
    }
}
