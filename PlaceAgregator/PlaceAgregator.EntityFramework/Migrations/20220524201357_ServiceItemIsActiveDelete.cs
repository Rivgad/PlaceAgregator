using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class ServiceItemIsActiveDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ServiceItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ServiceItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
