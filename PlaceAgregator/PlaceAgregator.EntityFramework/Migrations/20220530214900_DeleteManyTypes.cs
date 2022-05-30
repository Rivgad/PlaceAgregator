using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class DeleteManyTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_BuildingTypes_BuildingTypeId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_ParkingTypes_ParkingTypeId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_WaterTypes_WaterTypeId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "BuildingTypes");

            migrationBuilder.DropTable(
                name: "ParkingTypes");

            migrationBuilder.DropTable(
                name: "WaterTypes");

            migrationBuilder.DropIndex(
                name: "IX_Places_BuildingTypeId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_ParkingTypeId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_WaterTypeId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "BuildingTypeId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "ParkingTypeId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "WaterTypeId",
                table: "Places");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingTypeId",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingTypeId",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WaterTypeId",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BuildingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_BuildingTypeId",
                table: "Places",
                column: "BuildingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_ParkingTypeId",
                table: "Places",
                column: "ParkingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_WaterTypeId",
                table: "Places",
                column: "WaterTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_BuildingTypes_BuildingTypeId",
                table: "Places",
                column: "BuildingTypeId",
                principalTable: "BuildingTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_ParkingTypes_ParkingTypeId",
                table: "Places",
                column: "ParkingTypeId",
                principalTable: "ParkingTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_WaterTypes_WaterTypeId",
                table: "Places",
                column: "WaterTypeId",
                principalTable: "WaterTypes",
                principalColumn: "Id");
        }
    }
}
