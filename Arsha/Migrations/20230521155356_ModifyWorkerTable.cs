using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arsha.Migrations
{
    public partial class ModifyWorkerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Workers_WorkerId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_WorkerId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Workers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkerId",
                table: "Workers",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Workers_WorkerId",
                table: "Workers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
