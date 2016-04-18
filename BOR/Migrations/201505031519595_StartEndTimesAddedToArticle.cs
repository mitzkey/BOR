namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartEndTimesAddedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "StartDate", c => c.DateTime());
            AddColumn("dbo.Articles", "StartTime", c => c.DateTime());
            AddColumn("dbo.Articles", "EndTime", c => c.DateTime());
            DropColumn("dbo.Articles", "Start");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Start", c => c.DateTime());
            DropColumn("dbo.Articles", "EndTime");
            DropColumn("dbo.Articles", "StartTime");
            DropColumn("dbo.Articles", "StartDate");
        }
    }
}
