namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoivodshipObjectAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voivodships",
                c => new
                    {
                        VoivodshipID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.VoivodshipID);
            
            AddColumn("dbo.Articles", "VoivodshipID", c => c.Int());
            CreateIndex("dbo.Articles", "VoivodshipID");
            AddForeignKey("dbo.Articles", "VoivodshipID", "dbo.Voivodships", "VoivodshipID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Voivodship", c => c.String());
            DropForeignKey("dbo.Articles", "VoivodshipID", "dbo.Voivodships");
            DropIndex("dbo.Articles", new[] { "VoivodshipID" });
            DropColumn("dbo.Articles", "VoivodshipID");
            DropTable("dbo.Voivodships");
        }
    }
}
