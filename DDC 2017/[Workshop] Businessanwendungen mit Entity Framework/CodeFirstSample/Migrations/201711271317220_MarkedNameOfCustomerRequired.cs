namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkedNameOfCustomerRequired : DbMigration
    {
        public override void Up()
        {
            this.Sql("UPDATE dbo.Customers SET NAME ='unknown' WHERE NAME is null");
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
    }
}
