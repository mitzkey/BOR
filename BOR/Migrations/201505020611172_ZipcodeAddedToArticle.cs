namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZipcodeAddedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Zipcode");
        }
    }
}
