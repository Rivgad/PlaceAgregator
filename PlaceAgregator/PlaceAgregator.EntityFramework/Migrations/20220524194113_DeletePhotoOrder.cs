using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class DeletePhotoOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PlacePhotos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PlacePhotos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
