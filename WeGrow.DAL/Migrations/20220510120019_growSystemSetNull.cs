using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class growSystemSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grow_SystemInstance_System_Id",
                table: "Grow");

            migrationBuilder.AddForeignKey(
                name: "FK_Grow_SystemInstance_System_Id",
                table: "Grow",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grow_SystemInstance_System_Id",
                table: "Grow");

            migrationBuilder.AddForeignKey(
                name: "FK_Grow_SystemInstance_System_Id",
                table: "Grow",
                column: "System_Id",
                principalTable: "SystemInstance",
                principalColumn: "Id");
        }
    }
}
