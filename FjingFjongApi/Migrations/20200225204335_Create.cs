using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FjingFjongApi.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Rating = table.Column<double>(nullable: false, defaultValue: 1000.0),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeamOneScore = table.Column<int>(nullable: false),
                    TeamTwoScore = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PlayerOneId = table.Column<Guid>(nullable: false),
                    PlayerTwoId = table.Column<Guid>(nullable: false),
                    PlayerThreeId = table.Column<Guid>(nullable: false),
                    PlayerFourId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerFourId",
                        column: x => x.PlayerFourId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerThreeId",
                        column: x => x.PlayerThreeId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerFourId",
                table: "Matches",
                column: "PlayerFourId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerOneId",
                table: "Matches",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerThreeId",
                table: "Matches",
                column: "PlayerThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerTwoId",
                table: "Matches",
                column: "PlayerTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
