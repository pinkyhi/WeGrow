using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class LastResponseItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastResponseItem",
                table: "ModuleInstance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "LastResponseItem",
                table: "ModuleInstance");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id");
        }
    }
}
