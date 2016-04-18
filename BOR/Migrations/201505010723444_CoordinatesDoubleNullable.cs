namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoordinatesDoubleNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Longitude", c => c.Double());
            AlterColumn("dbo.Articles", "Latitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Articles", "Longitude", c => c.Double(nullable: false));
        }
    }
}
