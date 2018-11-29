namespace EF1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdFirstNameLastNameToCustmer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
