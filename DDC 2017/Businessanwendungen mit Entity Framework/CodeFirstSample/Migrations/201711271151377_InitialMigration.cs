namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderComment = c.String(),
                        AssignedCustomer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.AssignedCustomer_Id, cascadeDelete: true)
                .Index(t => t.AssignedCustomer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            DropForeignKey("dbo.Orders", "AssignedCustomer_Id", "dbo.Customers");
            DropForeignKey("dbo.OrderArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.OrderArticles", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderArticles", new[] { "Article_Id" });
            DropIndex("dbo.OrderArticles", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "AssignedCustomer_Id" });
            DropTable("dbo.OrderArticles");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Articles");
        }
    }
}
