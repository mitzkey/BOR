namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartEndDatesAddedToArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "StartDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Articles", "StartTime", c => c.DateTime());
            AddColumn("dbo.Articles", "End", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Articles", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "EndTime");
            DropColumn("dbo.Articles", "End");
            DropColumn("dbo.Articles", "StartTime");
            DropColumn("dbo.Articles", "StartDate");
        }
    }
}
