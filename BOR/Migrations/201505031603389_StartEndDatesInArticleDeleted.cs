namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartEndDatesInArticleDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "StartDate");
            DropColumn("dbo.Articles", "StartTime");
            DropColumn("dbo.Articles", "End");
            DropColumn("dbo.Articles", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "EndTime", c => c.DateTime());
            AddColumn("dbo.Articles", "End", c => c.DateTime());
            AddColumn("dbo.Articles", "StartTime", c => c.DateTime());
            AddColumn("dbo.Articles", "StartDate", c => c.DateTime());
        }
    }
}
