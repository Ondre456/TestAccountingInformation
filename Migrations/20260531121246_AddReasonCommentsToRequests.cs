using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAccountingInformation.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonCommentsToRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReasonComment",
                table: "Requests",
                type: "TEXT",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonComment",
                table: "Requests");
        }
    }
}
