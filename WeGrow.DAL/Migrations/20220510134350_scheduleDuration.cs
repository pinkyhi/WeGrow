using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class scheduleDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Schedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Grow",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Grow");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Schedule",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
