namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClicksAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clicks",
                c => new
                    {
                        ClickID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ArticleType = c.String(),
                    })
                .PrimaryKey(t => t.ClickID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clicks");
        }
    }
}
