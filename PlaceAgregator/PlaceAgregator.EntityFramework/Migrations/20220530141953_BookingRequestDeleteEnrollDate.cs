using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class BookingRequestDeleteEnrollDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollDateTime",
                table: "BookingRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollDateTime",
                table: "BookingRequests",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
