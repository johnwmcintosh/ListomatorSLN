using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listomator.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoGroups",
                columns: table => new
                {
                    ToDoGroupName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoGroups", x => x.ToDoGroupName);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    ToDoItemName = table.Column<string>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    UseDueDate = table.Column<bool>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    ToDoGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ToDoItemName);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoGroups_ToDoGroupName",
                        column: x => x.ToDoGroupName,
                        principalTable: "ToDoGroups",
                        principalColumn: "ToDoGroupName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoGroupName",
                table: "ToDoItems",
                column: "ToDoGroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoGroups");
        }
    }
}
