using Microsoft.EntityFrameworkCore.Migrations;

namespace FjingFjongApi.Migrations
{
    public partial class AddPlayerImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Players");
        }
    }
}
