namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediumIDAndArticleIDNotNullInVote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.Votes", "MediumID", "dbo.Media");
            DropIndex("dbo.Votes", new[] { "MediumID" });
            DropIndex("dbo.Votes", new[] { "ArticleID" });
            AlterColumn("dbo.Votes", "MediumID", c => c.Int());
            AlterColumn("dbo.Votes", "ArticleID", c => c.Int());
            CreateIndex("dbo.Votes", "MediumID");
            CreateIndex("dbo.Votes", "ArticleID");
            AddForeignKey("dbo.Votes", "ArticleID", "dbo.Articles", "ArticleID");
            AddForeignKey("dbo.Votes", "MediumID", "dbo.Media", "MediumID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "MediumID", "dbo.Media");
            DropForeignKey("dbo.Votes", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Votes", new[] { "ArticleID" });
            DropIndex("dbo.Votes", new[] { "MediumID" });
            AlterColumn("dbo.Votes", "ArticleID", c => c.Int(nullable: false));
            AlterColumn("dbo.Votes", "MediumID", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "ArticleID");
            CreateIndex("dbo.Votes", "MediumID");
            AddForeignKey("dbo.Votes", "MediumID", "dbo.Media", "MediumID", cascadeDelete: true);
            AddForeignKey("dbo.Votes", "ArticleID", "dbo.Articles", "ArticleID", cascadeDelete: true);
        }
    }
}
