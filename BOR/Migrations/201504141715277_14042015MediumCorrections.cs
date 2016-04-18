namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14042015MediumCorrections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Medium_MediumID", "dbo.Media");
            DropIndex("dbo.Articles", new[] { "Medium_MediumID" });
            AddColumn("dbo.Media", "AspectRatio", c => c.String());
            AddColumn("dbo.Media", "Article_ArticleID", c => c.Int());
            CreateIndex("dbo.Media", "Article_ArticleID");
            AddForeignKey("dbo.Media", "Article_ArticleID", "dbo.Articles", "ArticleID");
            DropColumn("dbo.Articles", "Medium_MediumID");
            DropColumn("dbo.Media", "AspectRation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "AspectRation", c => c.String());
            AddColumn("dbo.Articles", "Medium_MediumID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Media", "Article_ArticleID", "dbo.Articles");
            DropIndex("dbo.Media", new[] { "Article_ArticleID" });
            DropColumn("dbo.Media", "Article_ArticleID");
            DropColumn("dbo.Media", "AspectRatio");
            CreateIndex("dbo.Articles", "Medium_MediumID");
            AddForeignKey("dbo.Articles", "Medium_MediumID", "dbo.Media", "MediumID");
        }
    }
}
