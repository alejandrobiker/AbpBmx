using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyEvaluator.Migrations
{
    /// <inheritdoc />
    public partial class Addrelationshiptoevaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AthleteId",
                table: "CompetencyEvaluatorEvaluation1s",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluatorEvaluation1s_AthleteId",
                table: "CompetencyEvaluatorEvaluation1s",
                column: "AthleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencyEvaluatorEvaluation1s_CompetencyEvaluatorAthletes_AthleteId",
                table: "CompetencyEvaluatorEvaluation1s",
                column: "AthleteId",
                principalTable: "CompetencyEvaluatorAthletes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencyEvaluatorEvaluation1s_CompetencyEvaluatorAthletes_AthleteId",
                table: "CompetencyEvaluatorEvaluation1s");

            migrationBuilder.DropIndex(
                name: "IX_CompetencyEvaluatorEvaluation1s_AthleteId",
                table: "CompetencyEvaluatorEvaluation1s");

            migrationBuilder.DropColumn(
                name: "AthleteId",
                table: "CompetencyEvaluatorEvaluation1s");
        }
    }
}
