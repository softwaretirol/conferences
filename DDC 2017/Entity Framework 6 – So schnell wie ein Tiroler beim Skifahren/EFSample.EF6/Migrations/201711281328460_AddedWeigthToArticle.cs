namespace EFSample.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWeigthToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Weigth", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Weigth");
        }
    }
}
