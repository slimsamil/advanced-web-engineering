using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication7.Migrations
{
    public partial class svmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Betreuer");

            migrationBuilder.AddColumn<string>(
                name: "SupervisorEMail",
                table: "Thesen",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorEMail",
                table: "Thesen");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Betreuer",
                nullable: true);
        }
    }
}
