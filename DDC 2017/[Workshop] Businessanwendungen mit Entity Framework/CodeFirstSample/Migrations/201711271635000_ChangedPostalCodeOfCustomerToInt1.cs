namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPostalCodeOfCustomerToInt1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "AssignedCustomer_Id", newName: "AssignedCustomerId");
            RenameIndex(table: "dbo.Orders", name: "IX_AssignedCustomer_Id", newName: "IX_AssignedCustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_AssignedCustomerId", newName: "IX_AssignedCustomer_Id");
            RenameColumn(table: "dbo.Orders", name: "AssignedCustomerId", newName: "AssignedCustomer_Id");
        }
    }
}
