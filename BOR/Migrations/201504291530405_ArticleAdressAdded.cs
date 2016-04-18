namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleAdressAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Voivodship", c => c.String());
            AddColumn("dbo.Articles", "City", c => c.String());
            AddColumn("dbo.Articles", "Street", c => c.String());
            AddColumn("dbo.Articles", "HouseNumber", c => c.String());
            AddColumn("dbo.Articles", "ApartmentNumber", c => c.String());
            AddColumn("dbo.Articles", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Articles", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Latitude");
            DropColumn("dbo.Articles", "Longitude");
            DropColumn("dbo.Articles", "ApartmentNumber");
            DropColumn("dbo.Articles", "HouseNumber");
            DropColumn("dbo.Articles", "Street");
            DropColumn("dbo.Articles", "City");
            DropColumn("dbo.Articles", "Voivodship");
        }
    }
}
