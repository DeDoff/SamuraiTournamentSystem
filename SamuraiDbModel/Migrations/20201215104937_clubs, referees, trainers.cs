using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SamuraiDbModel.Migrations
{
    public partial class clubsrefereestrainers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitors_WinnerId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Sportsmens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportClubId",
                table: "Sportsmens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Sportsmens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Matches",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Competitors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferreeType = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referees_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referees_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SportClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClubId = table.Column<int>(type: "integer", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_SportClubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "SportClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sportsmens_SportClubId",
                table: "Sportsmens",
                column: "SportClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Sportsmens_TrainerId",
                table: "Sportsmens",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Referees_CompetitionId",
                table: "Referees",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Referees_MatchId",
                table: "Referees",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_ClubId",
                table: "Trainers",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitors_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Competitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sportsmens_SportClubs_SportClubId",
                table: "Sportsmens",
                column: "SportClubId",
                principalTable: "SportClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sportsmens_Trainers_TrainerId",
                table: "Sportsmens",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Competitors_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Sportsmens_SportClubs_SportClubId",
                table: "Sportsmens");

            migrationBuilder.DropForeignKey(
                name: "FK_Sportsmens_Trainers_TrainerId",
                table: "Sportsmens");

            migrationBuilder.DropTable(
                name: "Referees");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "SportClubs");

            migrationBuilder.DropIndex(
                name: "IX_Sportsmens_SportClubId",
                table: "Sportsmens");

            migrationBuilder.DropIndex(
                name: "IX_Sportsmens_TrainerId",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "SportClubId",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Sportsmens");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Competitors");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Competitors_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Competitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
