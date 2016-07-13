namespace DWXEFWPF.ConsoleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedPropertyCityOfPerson : DbMigration
    {
        public override void Up()
        {
            //RenameColumn();
            AddColumn("dbo.People", "Stadt", c => c.String());
            DropColumn("dbo.People", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "City", c => c.String());
            DropColumn("dbo.People", "Stadt");
        }
    }
}
