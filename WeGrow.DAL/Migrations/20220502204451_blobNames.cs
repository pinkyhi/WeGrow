using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class blobNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GifBlobName",
                table: "Grow",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "GifBlobName",
                table: "Grow");
        }
    }
}
