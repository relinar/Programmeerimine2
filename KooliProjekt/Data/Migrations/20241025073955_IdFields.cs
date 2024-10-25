using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NutrientsID",
                table: "nutrients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HealthDataID",
                table: "health_data",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "food_chartID",
                table: "food_Chart",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "nutrients",
                newName: "NutrientsID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "health_data",
                newName: "HealthDataID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "food_Chart",
                newName: "food_chartID");
        }
    }
}
