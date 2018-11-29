namespace EF1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleName = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleOrders",
                c => new
                    {
                        Article_Id = c.Int(nullable: false),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_Id, t.Order_Id })
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Article_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.ArticleOrders", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ArticleOrders", "Article_Id", "dbo.Articles");
            DropIndex("dbo.ArticleOrders", new[] { "Order_Id" });
            DropIndex("dbo.ArticleOrders", new[] { "Article_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropTable("dbo.ArticleOrders");
            DropTable("dbo.Articles");
            DropTable("dbo.Orders");
        }
    }
}
