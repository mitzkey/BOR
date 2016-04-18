namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherDatetimeArticleUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "StartTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Articles", "EndTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "EndTime", c => c.DateTime());
            AlterColumn("dbo.Articles", "StartTime", c => c.DateTime());
        }
    }
}
