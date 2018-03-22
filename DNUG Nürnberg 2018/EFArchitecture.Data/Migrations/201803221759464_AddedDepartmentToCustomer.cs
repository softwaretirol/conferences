namespace EFArchitecture.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepartmentToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Department");
        }
    }
}
