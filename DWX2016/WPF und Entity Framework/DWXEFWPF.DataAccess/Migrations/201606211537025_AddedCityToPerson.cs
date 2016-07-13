namespace DWXEFWPF.ConsoleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "City");
        }
    }
}
