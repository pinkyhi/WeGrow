using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    public partial class initAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Modules",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "Is_Public",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemInstance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Module_Id = table.Column<int>(type: "int", nullable: false),
                    Cache_System_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Cache_Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => new { x.Order_Id, x.Module_Id });
                    table.ForeignKey(
                        name: "FK_Receipt_Modules_Module_Id",
                        column: x => x.Module_Id,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receipt_Order_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleInstance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    System_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Module_Id = table.Column<int>(type: "int", nullable: false),
                    LastResponse = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleInstance_Modules_Module_Id",
                        column: x => x.Module_Id,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuleInstance_SystemInstance_System_Id",
                        column: x => x.System_Id,
                        principalTable: "SystemInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    System_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Is_Public = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_SystemInstance_System_Id",
                        column: x => x.System_Id,
                        principalTable: "SystemInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timelaps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedule_Id = table.Column<int>(type: "int", nullable: false),
                    System_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HistoryFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grow_Schedule_Schedule_Id",
                        column: x => x.Schedule_Id,
                        principalTable: "Schedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grow_SystemInstance_System_Id",
                        column: x => x.System_Id,
                        principalTable: "SystemInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grow_Schedule_Id",
                table: "Grow",
                column: "Schedule_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grow_System_Id",
                table: "Grow",
                column: "System_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleInstance_Module_Id_System_Id",
                table: "ModuleInstance",
                columns: new[] { "Module_Id", "System_Id" },
                unique: true,
                filter: "[System_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleInstance_System_Id",
                table: "ModuleInstance",
                column: "System_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_Module_Id",
                table: "Receipt",
                column: "Module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_System_Id",
                table: "Schedule",
                column: "System_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grow");

            migrationBuilder.DropTable(
                name: "ModuleInstance");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SystemInstance");

            migrationBuilder.DropColumn(
                name: "Is_Public",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Modules",
                newName: "Title");
        }
    }
}
