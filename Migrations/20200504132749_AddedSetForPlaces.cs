using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_Assignment_3.Migrations
{
    public partial class AddedSetForPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                columns: new[] { "Lat", "Lon" });

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Places_PlaceLat_PlaceLon",
                table: "Measurements",
                columns: new[] { "PlaceLat", "PlaceLon" },
                principalTable: "Places",
                principalColumns: new[] { "Lat", "Lon" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Places_PlaceLat_PlaceLon",
                table: "Measurements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                columns: new[] { "Lat", "Lon" });

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Place_PlaceLat_PlaceLon",
                table: "Measurements",
                columns: new[] { "PlaceLat", "PlaceLon" },
                principalTable: "Place",
                principalColumns: new[] { "Lat", "Lon" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
