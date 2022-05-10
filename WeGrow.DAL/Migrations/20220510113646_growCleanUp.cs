using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class growCleanUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timelaps",
                table: "Grow",
                newName: "TimelapsBlobName");

            migrationBuilder.RenameColumn(
                name: "HistoryFile",
                table: "Grow",
                newName: "TimelapsBlobLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimelapsBlobName",
                table: "Grow",
                newName: "Timelaps");

            migrationBuilder.RenameColumn(
                name: "TimelapsBlobLink",
                table: "Grow",
                newName: "HistoryFile");
        }
    }
}
