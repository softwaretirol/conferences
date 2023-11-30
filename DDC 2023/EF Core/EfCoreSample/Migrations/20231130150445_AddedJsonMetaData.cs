using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreSample.Migrations
{
    /// <inheritdoc />
    public partial class AddedJsonMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "ArticleCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "ArticleCategories");
        }
    }
}
