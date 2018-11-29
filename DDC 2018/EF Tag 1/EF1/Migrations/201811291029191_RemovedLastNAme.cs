namespace EF1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLastNAme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "LastName", c => c.String());
        }
    }
}
