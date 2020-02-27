using Microsoft.EntityFrameworkCore.Migrations;

namespace BASTAEF.Migrations
{
    public partial class AddCountryToSpeaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Speakers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Speakers");
        }
    }
}
