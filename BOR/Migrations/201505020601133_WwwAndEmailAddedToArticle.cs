namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WwwAndEmailAddedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Www", c => c.String());
            AddColumn("dbo.Articles", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Email");
            DropColumn("dbo.Articles", "Www");
        }
    }
}
