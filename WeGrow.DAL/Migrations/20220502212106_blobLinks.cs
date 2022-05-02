using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class blobLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlobName",
                table: "Modules",
                newName: "BlobLink");

            migrationBuilder.RenameColumn(
                name: "GifBlobName",
                table: "Grow",
                newName: "BlobLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlobLink",
                table: "Modules",
                newName: "BlobName");

            migrationBuilder.RenameColumn(
                name: "BlobLink",
                table: "Grow",
                newName: "GifBlobName");
        }
    }
}
