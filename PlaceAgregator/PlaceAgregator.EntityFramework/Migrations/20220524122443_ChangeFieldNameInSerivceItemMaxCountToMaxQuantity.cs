using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class ChangeFieldNameInSerivceItemMaxCountToMaxQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxCount",
                table: "ServiceItems",
                newName: "MaxQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxQuantity",
                table: "ServiceItems",
                newName: "MaxCount");
        }
    }
}
