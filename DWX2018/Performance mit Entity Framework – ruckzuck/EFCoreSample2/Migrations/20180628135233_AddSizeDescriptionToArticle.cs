using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSample2.Migrations
{
    public partial class AddSizeDescriptionToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeDescription",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeDescription",
                table: "Articles");
        }
    }
}
