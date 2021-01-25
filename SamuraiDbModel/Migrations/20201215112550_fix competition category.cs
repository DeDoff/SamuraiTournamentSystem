using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiDbModel.Migrations
{
    public partial class fixcompetitioncategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionCategories_CompositeCategories_CompositeCategory~",
                table: "CompetitionCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CompositeCategoryId",
                table: "CompetitionCategories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionCategories_CompositeCategories_CompositeCategory~",
                table: "CompetitionCategories",
                column: "CompositeCategoryId",
                principalTable: "CompositeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionCategories_CompositeCategories_CompositeCategory~",
                table: "CompetitionCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CompositeCategoryId",
                table: "CompetitionCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionCategories_CompositeCategories_CompositeCategory~",
                table: "CompetitionCategories",
                column: "CompositeCategoryId",
                principalTable: "CompositeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
