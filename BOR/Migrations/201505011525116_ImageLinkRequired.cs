namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageLinkRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Media", "Link", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Media", "Link", c => c.String());
        }
    }
}
