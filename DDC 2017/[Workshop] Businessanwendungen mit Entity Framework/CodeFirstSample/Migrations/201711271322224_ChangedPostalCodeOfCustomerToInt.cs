namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPostalCodeOfCustomerToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PostalCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PostalCode", c => c.String());
        }
    }
}
