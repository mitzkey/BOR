namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotesOrganised : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VoteMediums", "Vote_VoteID", "dbo.Votes");
            DropForeignKey("dbo.VoteMediums", "Medium_MediumID", "dbo.Media");
            DropIndex("dbo.VoteMediums", new[] { "Vote_VoteID" });
            DropIndex("dbo.VoteMediums", new[] { "Medium_MediumID" });
            AddColumn("dbo.Votes", "ArticleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "MediumID");
            CreateIndex("dbo.Votes", "ArticleID");
            AddForeignKey("dbo.Votes", "ArticleID", "dbo.Articles", "ArticleID", cascadeDelete: true);
            AddForeignKey("dbo.Votes", "MediumID", "dbo.Media", "MediumID", cascadeDelete: true);
            DropTable("dbo.VoteMediums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VoteMediums",
                c => new
                    {
                        Vote_VoteID = c.Int(nullable: false),
                        Medium_MediumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_VoteID, t.Medium_MediumID });
            
            DropForeignKey("dbo.Votes", "MediumID", "dbo.Media");
            DropForeignKey("dbo.Votes", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Votes", new[] { "ArticleID" });
            DropIndex("dbo.Votes", new[] { "MediumID" });
            DropColumn("dbo.Votes", "ArticleID");
            CreateIndex("dbo.VoteMediums", "Medium_MediumID");
            CreateIndex("dbo.VoteMediums", "Vote_VoteID");
            AddForeignKey("dbo.VoteMediums", "Medium_MediumID", "dbo.Media", "MediumID", cascadeDelete: true);
            AddForeignKey("dbo.VoteMediums", "Vote_VoteID", "dbo.Votes", "VoteID", cascadeDelete: true);
        }
    }
}
