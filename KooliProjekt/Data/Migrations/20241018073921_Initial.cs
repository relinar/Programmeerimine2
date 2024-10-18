using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "amount",
                columns: table => new
                {
                    AmountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutrientsID = table.Column<int>(type: "int", nullable: false),
                    AmountDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amount", x => x.AmountID);
                });

            migrationBuilder.CreateTable(
                name: "food_Chart",
                columns: table => new
                {
                    food_chartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    meal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nutrients = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_Chart", x => x.food_chartID);
                });

            migrationBuilder.CreateTable(
                name: "health_data",
                columns: table => new
                {
                    HealthDataID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodSugar = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    BloodAir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Systolic = table.Column<float>(type: "real", nullable: false),
                    Diastolic = table.Column<float>(type: "real", nullable: false),
                    Pulse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_health_data", x => x.HealthDataID);
                });

            migrationBuilder.CreateTable(
                name: "nutrients",
                columns: table => new
                {
                    NutrientsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carbohydrates = table.Column<float>(type: "real", nullable: false),
                    Sugars = table.Column<float>(type: "real", nullable: false),
                    Fats = table.Column<float>(type: "real", nullable: false),
                    FoodChart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutrients", x => x.NutrientsID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailySummary = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Meal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amount");

            migrationBuilder.DropTable(
                name: "food_Chart");

            migrationBuilder.DropTable(
                name: "health_data");

            migrationBuilder.DropTable(
                name: "nutrients");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
