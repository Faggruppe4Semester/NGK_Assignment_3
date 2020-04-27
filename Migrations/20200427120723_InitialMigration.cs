using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_Assignment_3.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => new { x.Lat, x.Lon });
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Time = table.Column<DateTime>(nullable: false),
                    PlaceLat = table.Column<double>(nullable: true),
                    PlaceLon = table.Column<double>(nullable: true),
                    Temperature = table.Column<float>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    Pressure = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Time);
                    table.ForeignKey(
                        name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                        columns: x => new { x.PlaceLat, x.PlaceLon },
                        principalTable: "Place",
                        principalColumns: new[] { "Lat", "Lon" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PlaceLat_PlaceLon",
                table: "Measurements",
                columns: new[] { "PlaceLat", "PlaceLon" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Place");
        }
    }
}
