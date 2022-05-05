using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class ModuleUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleInstance_Module_Id_System_Id",
                table: "ModuleInstance");

            migrationBuilder.DropColumn(
                name: "Cache_System_Id",
                table: "Receipt");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "ModuleInstance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleInstance_Module_Id",
                table: "ModuleInstance",
                column: "Module_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleInstance_Module_Id",
                table: "ModuleInstance");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "ModuleInstance");

            migrationBuilder.AddColumn<string>(
                name: "Cache_System_Id",
                table: "Receipt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleInstance_Module_Id_System_Id",
                table: "ModuleInstance",
                columns: new[] { "Module_Id", "System_Id" },
                unique: true,
                filter: "[System_Id] IS NOT NULL");
        }
    }
}
