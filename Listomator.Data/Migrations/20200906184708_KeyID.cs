using Microsoft.EntityFrameworkCore.Migrations;

namespace Listomator.Data.Migrations
{
    public partial class KeyID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoGroups_ToDoGroupName",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoGroupName",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoGroups",
                table: "ToDoGroups");

            migrationBuilder.DropColumn(
                name: "ToDoGroupName",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<string>(
                name: "ToDoItemName",
                table: "ToDoItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ToDoItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ToDoGroupId",
                table: "ToDoItems",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToDoGroupName",
                table: "ToDoGroups",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ToDoGroups",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoGroups",
                table: "ToDoGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoGroupId",
                table: "ToDoItems",
                column: "ToDoGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoGroups_ToDoGroupId",
                table: "ToDoItems",
                column: "ToDoGroupId",
                principalTable: "ToDoGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoGroups_ToDoGroupId",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_ToDoGroupId",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoGroups",
                table: "ToDoGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "ToDoGroupId",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ToDoGroups");

            migrationBuilder.AlterColumn<string>(
                name: "ToDoItemName",
                table: "ToDoItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToDoGroupName",
                table: "ToDoItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToDoGroupName",
                table: "ToDoGroups",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "ToDoItemName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoGroups",
                table: "ToDoGroups",
                column: "ToDoGroupName");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoGroupName",
                table: "ToDoItems",
                column: "ToDoGroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoGroups_ToDoGroupName",
                table: "ToDoItems",
                column: "ToDoGroupName",
                principalTable: "ToDoGroups",
                principalColumn: "ToDoGroupName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
