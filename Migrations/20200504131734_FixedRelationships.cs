using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_Assignment_3.Migrations
{
    public partial class FixedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements");

            migrationBuilder.AlterColumn<double>(
                name: "PlaceLon",
                table: "Measurements",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PlaceLat",
                table: "Measurements",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements",
                columns: new[] { "PlaceLat", "PlaceLon" },
                principalTable: "Place",
                principalColumns: new[] { "Lat", "Lon" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements");

            migrationBuilder.AlterColumn<double>(
                name: "PlaceLon",
                table: "Measurements",
                type: "float",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "PlaceLat",
                table: "Measurements",
                type: "float",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements",
                columns: new[] { "PlaceLat", "PlaceLon" },
                principalTable: "Place",
                principalColumns: new[] { "Lat", "Lon" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
