using Microsoft.EntityFrameworkCore.Migrations;

namespace FjingFjongApi.Migrations
{
    public partial class CleanUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "API_KEY",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Users",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "API_KEY",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
