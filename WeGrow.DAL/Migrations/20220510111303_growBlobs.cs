using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class growBlobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlobName",
                table: "Grow",
                newName: "ScheduleBlobName");

            migrationBuilder.RenameColumn(
                name: "BlobLink",
                table: "Grow",
                newName: "ScheduleBlobLink");

            migrationBuilder.AddColumn<string>(
                name: "GrowBlobLink",
                table: "Grow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrowBlobName",
                table: "Grow",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowBlobLink",
                table: "Grow");

            migrationBuilder.DropColumn(
                name: "GrowBlobName",
                table: "Grow");

            migrationBuilder.RenameColumn(
                name: "ScheduleBlobName",
                table: "Grow",
                newName: "BlobName");

            migrationBuilder.RenameColumn(
                name: "ScheduleBlobLink",
                table: "Grow",
                newName: "BlobLink");
        }
    }
}
