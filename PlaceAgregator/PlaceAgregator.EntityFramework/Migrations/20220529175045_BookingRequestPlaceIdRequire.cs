using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class BookingRequestPlaceIdRequire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "BookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "BookingRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
