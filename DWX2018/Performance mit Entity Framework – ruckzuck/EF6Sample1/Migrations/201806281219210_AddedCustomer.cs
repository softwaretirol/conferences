namespace EF6Sample1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Articles", "Customer_Id", c => c.Int());
            AddColumn("dbo.Orders", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "Customer_Id");
            CreateIndex("dbo.Orders", "Customer_Id");
            AddForeignKey("dbo.Articles", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Articles", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Articles", new[] { "Customer_Id" });
            DropColumn("dbo.Orders", "Customer_Id");
            DropColumn("dbo.Articles", "Customer_Id");
            DropTable("dbo.Customers");
        }
    }
}
