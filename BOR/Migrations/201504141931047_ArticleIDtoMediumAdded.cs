namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleIDtoMediumAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "ArticleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "ArticleID");
        }
    }
}
