using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class schedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_System_Id",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "System_Id",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "Schedule",
                newName: "BlobName");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "SystemInstance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlobLink",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemInstance_ScheduleId",
                table: "SystemInstance",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemInstance_Schedule_ScheduleId",
                table: "SystemInstance",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemInstance_Schedule_ScheduleId",
                table: "SystemInstance");

            migrationBuilder.DropIndex(
                name: "IX_SystemInstance_ScheduleId",
                table: "SystemInstance");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "SystemInstance");

            migrationBuilder.DropColumn(
                name: "BlobLink",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "BlobName",
                table: "Schedule",
                newName: "File");

            migrationBuilder.AddColumn<string>(
                name: "System_Id",
                table: "Schedule",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_System_Id",
                table: "Schedule",
                column: "System_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_SystemInstance_System_Id",
                table: "Schedule",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
