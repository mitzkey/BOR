namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartStopNullableInArticle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Start", c => c.DateTime());
            AlterColumn("dbo.Articles", "End", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "End", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "Start", c => c.DateTime(nullable: false));
        }
    }
}
