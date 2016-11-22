namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedHeadlineProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Headline", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Headline");
        }
    }
}
