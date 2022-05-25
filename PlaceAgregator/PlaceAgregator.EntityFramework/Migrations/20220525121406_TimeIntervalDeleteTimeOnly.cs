using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class TimeIntervalDeleteTimeOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeInterval_EndTime",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "TimeInterval_StartTime",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "TimeInterval_EndTime",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "TimeInterval_StartTime",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "TimeInterval_EndTime",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "TimeInterval_StartTime",
                table: "Charges");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDate",
                table: "Rates",
                newName: "TimeInterval_StartDateTime");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDate",
                table: "Rates",
                newName: "TimeInterval_EndDateTime");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDate",
                table: "Discounts",
                newName: "TimeInterval_StartDateTime");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDate",
                table: "Discounts",
                newName: "TimeInterval_EndDateTime");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDate",
                table: "Charges",
                newName: "TimeInterval_StartDateTime");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDate",
                table: "Charges",
                newName: "TimeInterval_EndDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDateTime",
                table: "Rates",
                newName: "TimeInterval_StartDate");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDateTime",
                table: "Rates",
                newName: "TimeInterval_EndDate");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDateTime",
                table: "Discounts",
                newName: "TimeInterval_StartDate");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDateTime",
                table: "Discounts",
                newName: "TimeInterval_EndDate");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_StartDateTime",
                table: "Charges",
                newName: "TimeInterval_StartDate");

            migrationBuilder.RenameColumn(
                name: "TimeInterval_EndDateTime",
                table: "Charges",
                newName: "TimeInterval_EndDate");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_EndTime",
                table: "Rates",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_StartTime",
                table: "Rates",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_EndTime",
                table: "Discounts",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_StartTime",
                table: "Discounts",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_EndTime",
                table: "Charges",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeInterval_StartTime",
                table: "Charges",
                type: "time without time zone",
                nullable: true);
        }
    }
}
