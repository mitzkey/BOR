namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MediumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoteID)
                .ForeignKey("dbo.Media", t => t.MediumID, cascadeDelete: true)
                .Index(t => t.MediumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "MediumID", "dbo.Media");
            DropIndex("dbo.Votes", new[] { "MediumID" });
            DropTable("dbo.Votes");
        }
    }
}
