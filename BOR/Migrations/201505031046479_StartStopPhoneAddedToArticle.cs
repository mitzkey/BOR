namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartStopPhoneAddedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "PhoneNo", c => c.String());
            AddColumn("dbo.Articles", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Articles", "End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "End");
            DropColumn("dbo.Articles", "Start");
            DropColumn("dbo.Articles", "PhoneNo");
        }
    }
}
