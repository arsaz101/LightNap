using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNap.DataProviders.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBicycleCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BicycleCategory",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "BicycleCategories",
                table: "Articles",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BicycleCategories",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "BicycleCategory",
                table: "Articles",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
