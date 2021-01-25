using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SamuraiDbModel.Migrations
{
    public partial class tournamentgrid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sportsmens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sportsmens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CompositeCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionCategories_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionCategories_CompositeCategories_CompositeCategory~",
                        column: x => x.CompositeCategoryId,
                        principalTable: "CompositeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentGrids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetitionCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentGrids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentGrids_CompetitionCategories_CompetitionCategoryId",
                        column: x => x.CompetitionCategoryId,
                        principalTable: "CompetitionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportsmanId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionCategoryId = table.Column<int>(type: "integer", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitors_CompetitionCategories_CompetitionCategoryId",
                        column: x => x.CompetitionCategoryId,
                        principalTable: "CompetitionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitors_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitors_Sportsmens_SportsmanId",
                        column: x => x.SportsmanId,
                        principalTable: "Sportsmens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchTournamentNumber = table.Column<int>(type: "integer", nullable: false),
                    MatchDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MatchInfo = table.Column<string>(type: "text", nullable: true),
                    WinnerId = table.Column<int>(type: "integer", nullable: false),
                    TournamentGridId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Competitors_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_TournamentGrids_TournamentGridId",
                        column: x => x.TournamentGridId,
                        principalTable: "TournamentGrids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportCategories_Name",
                table: "SportCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionCategories_CompetitionId",
                table: "CompetitionCategories",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionCategories_CompositeCategoryId",
                table: "CompetitionCategories",
                column: "CompositeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_CompetitionCategoryId",
                table: "Competitors",
                column: "CompetitionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_CompetitionId",
                table: "Competitors",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_MatchId",
                table: "Competitors",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_SportsmanId",
                table: "Competitors",
                column: "SportsmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentGridId",
                table: "Matches",
                column: "TournamentGridId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentGrids_CompetitionCategoryId",
                table: "TournamentGrids",
                column: "CompetitionCategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Competitors_Matches_MatchId",
                table: "Competitors",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionCategories_Competitions_CompetitionId",
                table: "CompetitionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitors_Competitions_CompetitionId",
                table: "Competitors");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitors_CompetitionCategories_CompetitionCategoryId",
                table: "Competitors");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentGrids_CompetitionCategories_CompetitionCategoryId",
                table: "TournamentGrids");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitors_Matches_MatchId",
                table: "Competitors");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "CompetitionCategories");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Competitors");

            migrationBuilder.DropTable(
                name: "TournamentGrids");

            migrationBuilder.DropTable(
                name: "Sportsmens");

            migrationBuilder.DropIndex(
                name: "IX_SportCategories_Name",
                table: "SportCategories");
        }
    }
}
