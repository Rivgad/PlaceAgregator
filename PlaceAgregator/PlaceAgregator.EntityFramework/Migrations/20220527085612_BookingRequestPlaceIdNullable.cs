using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class BookingRequestPlaceIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Places_PlaceId",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "IsCountable",
                table: "ServiceItems");

            migrationBuilder.AlterColumn<int>(
                name: "MaxQuantity",
                table: "ServiceItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "BookingRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Places_PlaceId",
                table: "BookingRequests",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRequests_Places_PlaceId",
                table: "BookingRequests");

            migrationBuilder.AlterColumn<int>(
                name: "MaxQuantity",
                table: "ServiceItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCountable",
                table: "ServiceItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "BookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRequests_Places_PlaceId",
                table: "BookingRequests",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
