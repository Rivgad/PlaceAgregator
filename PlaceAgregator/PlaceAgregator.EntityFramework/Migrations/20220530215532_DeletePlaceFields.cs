using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class DeletePlaceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FemaleToiletsQuantity",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "FloorsQuantity",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "HasDisabledEntrance",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "HasFreightElevator",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MaleToiletsQuantity",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "ParkingSpace",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "SharedToiletsQuantity",
                table: "Places",
                newName: "ToiletsQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToiletsQuantity",
                table: "Places",
                newName: "SharedToiletsQuantity");

            migrationBuilder.AddColumn<int>(
                name: "FemaleToiletsQuantity",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FloorsQuantity",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDisabledEntrance",
                table: "Places",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFreightElevator",
                table: "Places",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaleToiletsQuantity",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpace",
                table: "Places",
                type: "integer",
                nullable: true);
        }
    }
}
