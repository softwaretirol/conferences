namespace EF6Sample1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttributestoArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Description");
        }
    }
}
