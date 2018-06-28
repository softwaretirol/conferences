namespace EF6Sample1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            // ADO.NET SQLConnection

            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderArticles",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Article_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.OrderArticles", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderArticles", new[] { "Article_Id" });
            DropIndex("dbo.OrderArticles", new[] { "Order_Id" });
            DropTable("dbo.OrderArticles");
            DropTable("dbo.Orders");
            DropTable("dbo.Articles");
        }
    }
}
