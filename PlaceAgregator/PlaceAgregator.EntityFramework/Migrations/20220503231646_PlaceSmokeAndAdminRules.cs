using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class PlaceSmokeAndAdminRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministratorRule",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmokingRule",
                table: "Places",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministratorRule",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SmokingRule",
                table: "Places");
        }
    }
}
