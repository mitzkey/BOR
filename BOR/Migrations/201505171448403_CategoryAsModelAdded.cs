namespace BOR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAsModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("dbo.Articles", "CategoryID", c => c.Int());
            CreateIndex("dbo.Articles", "CategoryID");
            AddForeignKey("dbo.Articles", "CategoryID", "dbo.Categories", "CategoryID");
            DropColumn("dbo.Articles", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Category", c => c.String());
            DropForeignKey("dbo.Articles", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "CategoryID" });
            DropColumn("dbo.Articles", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
