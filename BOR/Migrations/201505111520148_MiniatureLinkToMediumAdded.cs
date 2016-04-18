namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiniatureLinkToMediumAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "MiniatureLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "MiniatureLink");
        }
    }
}
