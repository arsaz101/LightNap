using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNap.DataProviders.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ArticleCategory = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BicycleCategory = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Material = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LengthMm = table.Column<decimal>(type: "TEXT", nullable: true),
                    WidthMm = table.Column<decimal>(type: "TEXT", nullable: true),
                    HeightMm = table.Column<decimal>(type: "TEXT", nullable: true),
                    NetWeightG = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleNumber",
                table: "Articles",
                column: "ArticleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCategory",
                table: "Articles",
                column: "ArticleCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_BicycleCategory",
                table: "Articles",
                column: "BicycleCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Material",
                table: "Articles",
                column: "Material");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
} 