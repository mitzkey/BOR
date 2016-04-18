namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateAddedAsDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "DateAdded", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
