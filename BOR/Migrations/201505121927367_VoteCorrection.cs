namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoteCorrection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "MediumID", "dbo.Media");
            DropIndex("dbo.Votes", new[] { "MediumID" });
            CreateTable(
                "dbo.VoteMediums",
                c => new
                    {
                        Vote_VoteID = c.Int(nullable: false),
                        Medium_MediumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_VoteID, t.Medium_MediumID })
                .ForeignKey("dbo.Votes", t => t.Vote_VoteID, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.Medium_MediumID, cascadeDelete: true)
                .Index(t => t.Vote_VoteID)
                .Index(t => t.Medium_MediumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoteMediums", "Medium_MediumID", "dbo.Media");
            DropForeignKey("dbo.VoteMediums", "Vote_VoteID", "dbo.Votes");
            DropIndex("dbo.VoteMediums", new[] { "Medium_MediumID" });
            DropIndex("dbo.VoteMediums", new[] { "Vote_VoteID" });
            DropTable("dbo.VoteMediums");
            CreateIndex("dbo.Votes", "MediumID");
            AddForeignKey("dbo.Votes", "MediumID", "dbo.Media", "MediumID", cascadeDelete: true);
        }
    }
}
