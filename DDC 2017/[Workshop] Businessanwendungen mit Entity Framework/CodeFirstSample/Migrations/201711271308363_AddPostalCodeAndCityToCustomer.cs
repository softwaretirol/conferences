namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostalCodeAndCityToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "PostalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PostalCode");
            DropColumn("dbo.Customers", "City");
        }
    }
}
