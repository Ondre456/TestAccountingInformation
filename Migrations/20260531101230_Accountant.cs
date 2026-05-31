using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAccountingInformation.Migrations
{
    /// <inheritdoc />
    public partial class Accountant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExecutorId",
                table: "Requests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ExecutorId",
                table: "Requests",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ExecutorId",
                table: "Requests",
                column: "ExecutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ExecutorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ExecutorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Requests");
        }
    }
}
