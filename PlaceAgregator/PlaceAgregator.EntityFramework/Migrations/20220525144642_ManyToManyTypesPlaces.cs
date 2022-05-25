using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class ManyToManyTypesPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTypes_Places_PlaceId",
                table: "EventTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Prohibitions_Places_PlaceId",
                table: "Prohibitions");

            migrationBuilder.DropIndex(
                name: "IX_Prohibitions_PlaceId",
                table: "Prohibitions");

            migrationBuilder.DropIndex(
                name: "IX_EventTypes_PlaceId",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Prohibitions");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "EventTypes");

            migrationBuilder.CreateTable(
                name: "EventTypePlace",
                columns: table => new
                {
                    EventTypesId = table.Column<int>(type: "integer", nullable: false),
                    PlacesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypePlace", x => new { x.EventTypesId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_EventTypePlace_EventTypes_EventTypesId",
                        column: x => x.EventTypesId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTypePlace_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceProhibition",
                columns: table => new
                {
                    PlacesId = table.Column<int>(type: "integer", nullable: false),
                    ProhibitionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceProhibition", x => new { x.PlacesId, x.ProhibitionsId });
                    table.ForeignKey(
                        name: "FK_PlaceProhibition_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceProhibition_Prohibitions_ProhibitionsId",
                        column: x => x.ProhibitionsId,
                        principalTable: "Prohibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTypePlace_PlacesId",
                table: "EventTypePlace",
                column: "PlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceProhibition_ProhibitionsId",
                table: "PlaceProhibition",
                column: "ProhibitionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTypePlace");

            migrationBuilder.DropTable(
                name: "PlaceProhibition");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Prohibitions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "EventTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prohibitions_PlaceId",
                table: "Prohibitions",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTypes_PlaceId",
                table: "EventTypes",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTypes_Places_PlaceId",
                table: "EventTypes",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prohibitions_Places_PlaceId",
                table: "Prohibitions",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }
    }
}
