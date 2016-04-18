namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Title = c.String(),
                        Text = c.String(),
                        Author = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Medium_MediumID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Media", t => t.Medium_MediumID)
                .Index(t => t.Medium_MediumID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediumID = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        Name = c.String(),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Link = c.String(),
                        AspectRation = c.String(),
                    })
                .PrimaryKey(t => t.MediumID);
            
            CreateTable(
                "dbo.Articles_Comments",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Events",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Galleries",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Images",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Medium_MediumID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Medium_MediumID })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.Medium_MediumID, cascadeDelete: true)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Medium_MediumID);
            
            CreateTable(
                "dbo.Articles_OffRoadGirls",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_RelatedArticles",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Services",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Tutorials",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
            CreateTable(
                "dbo.Articles_Videos",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Medium_MediumID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Medium_MediumID })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.Medium_MediumID, cascadeDelete: true)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Medium_MediumID);
            
            CreateTable(
                "dbo.Articles_Workshops",
                c => new
                    {
                        Article_ArticleID = c.Int(nullable: false),
                        Article_ArticleID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ArticleID, t.Article_ArticleID1 })
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleID1)
                .Index(t => t.Article_ArticleID)
                .Index(t => t.Article_ArticleID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles_Workshops", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Workshops", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Videos", "Medium_MediumID", "dbo.Media");
            DropForeignKey("dbo.Articles_Videos", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Tutorials", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Tutorials", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Services", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Services", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_RelatedArticles", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_RelatedArticles", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_OffRoadGirls", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_OffRoadGirls", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Images", "Medium_MediumID", "dbo.Media");
            DropForeignKey("dbo.Articles_Images", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles", "Medium_MediumID", "dbo.Media");
            DropForeignKey("dbo.Articles_Galleries", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Galleries", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Events", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Events", "Article_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Articles_Comments", "Article_ArticleID1", "dbo.Articles");
            DropForeignKey("dbo.Articles_Comments", "Article_ArticleID", "dbo.Articles");
            DropIndex("dbo.Articles_Workshops", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Workshops", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Videos", new[] { "Medium_MediumID" });
            DropIndex("dbo.Articles_Videos", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Tutorials", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Tutorials", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Services", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Services", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_RelatedArticles", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_RelatedArticles", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_OffRoadGirls", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_OffRoadGirls", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Images", new[] { "Medium_MediumID" });
            DropIndex("dbo.Articles_Images", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Galleries", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Galleries", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Events", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Events", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles_Comments", new[] { "Article_ArticleID1" });
            DropIndex("dbo.Articles_Comments", new[] { "Article_ArticleID" });
            DropIndex("dbo.Articles", new[] { "Medium_MediumID" });
            DropTable("dbo.Articles_Workshops");
            DropTable("dbo.Articles_Videos");
            DropTable("dbo.Articles_Tutorials");
            DropTable("dbo.Articles_Services");
            DropTable("dbo.Articles_RelatedArticles");
            DropTable("dbo.Articles_OffRoadGirls");
            DropTable("dbo.Articles_Images");
            DropTable("dbo.Articles_Galleries");
            DropTable("dbo.Articles_Events");
            DropTable("dbo.Articles_Comments");
            DropTable("dbo.Media");
            DropTable("dbo.Articles");
        }
    }
}
