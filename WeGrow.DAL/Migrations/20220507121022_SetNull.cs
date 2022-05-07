using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class SetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleInstance_SystemInstance_System_Id",
                table: "ModuleInstance");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleInstance_SystemInstance_System_Id",
                table: "ModuleInstance",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleInstance_SystemInstance_System_Id",
                table: "ModuleInstance");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleInstance_SystemInstance_System_Id",
                table: "ModuleInstance",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id");
        }
    }
}
