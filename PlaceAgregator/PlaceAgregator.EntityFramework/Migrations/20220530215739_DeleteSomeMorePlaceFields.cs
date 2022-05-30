using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class DeleteSomeMorePlaceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellingHeight",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "HasElevator",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SocketsQuantity",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "ToiletsQuantity",
                table: "Places");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CellingHeight",
                table: "Places",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElevator",
                table: "Places",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SocketsQuantity",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToiletsQuantity",
                table: "Places",
                type: "integer",
                nullable: true);
        }
    }
}
