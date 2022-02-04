using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLibrary.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_User_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_Task_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_User_Id",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "SprintTasks");

            migrationBuilder.AddColumn<string>(
                name: "Sprint_Id",
                table: "Tasks",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Sprint_Id",
                table: "Tasks",
                column: "Sprint_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_User_Id",
                table: "Comments",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_Task_Id",
                table: "Comments",
                column: "Task_Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_User_Id",
                table: "Tasks",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sprints_Sprint_Id",
                table: "Tasks",
                column: "Sprint_Id",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_User_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_Task_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_User_Id",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sprints_Sprint_Id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Sprint_Id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Sprint_Id",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "SprintTasks",
                columns: table => new
                {
                    Sprint_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Task_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintTasks", x => new { x.Sprint_Id, x.Task_Id });
                    table.ForeignKey(
                        name: "FK_SprintTasks_Sprints_Sprint_Id",
                        column: x => x.Sprint_Id,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintTasks_Tasks_Task_Id",
                        column: x => x.Task_Id,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SprintTasks_Task_Id",
                table: "SprintTasks",
                column: "Task_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_User_Id",
                table: "Comments",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_Task_Id",
                table: "Comments",
                column: "Task_Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_User_Id",
                table: "Tasks",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
