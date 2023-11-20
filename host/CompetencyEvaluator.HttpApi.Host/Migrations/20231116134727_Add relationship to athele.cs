using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyEvaluator.Migrations
{
    /// <inheritdoc />
    public partial class Addrelationshiptoathele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "CompetencyEvaluatorAthletes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "CompetencyEvaluatorAthletes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluatorAthletes_CategoryId",
                table: "CompetencyEvaluatorAthletes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluatorAthletes_GenderId",
                table: "CompetencyEvaluatorAthletes",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyEvaluatorAthletes_CompetencyEvaluatorCategories_CategoryId",
                table: "CompetencyEvaluatorAthletes",
                column: "CategoryId",
                principalTable: "CompetencyEvaluatorCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyEvaluatorAthletes_CompetencyEvaluatorGenders_GenderId",
                table: "CompetencyEvaluatorAthletes",
                column: "GenderId",
                principalTable: "CompetencyEvaluatorGenders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyEvaluatorAthletes_CompetencyEvaluatorCategories_CategoryId",
                table: "CompetencyEvaluatorAthletes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyEvaluatorAthletes_CompetencyEvaluatorGenders_GenderId",
                table: "CompetencyEvaluatorAthletes");

            migrationBuilder.DropIndex(
                name: "IX_CompetencyEvaluatorAthletes_CategoryId",
                table: "CompetencyEvaluatorAthletes");

            migrationBuilder.DropIndex(
                name: "IX_CompetencyEvaluatorAthletes_GenderId",
                table: "CompetencyEvaluatorAthletes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CompetencyEvaluatorAthletes");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "CompetencyEvaluatorAthletes");
        }
    }
}
