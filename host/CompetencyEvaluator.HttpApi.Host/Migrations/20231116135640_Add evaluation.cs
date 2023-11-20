using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyEvaluator.Migrations
{
    /// <inheritdoc />
    public partial class Addevaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetencyEvaluatorEvaluation1s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Criterio_1_R1 = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Criterio_1_R2 = table.Column<double>(type: "float", nullable: false),
                    Criterio_2_R1 = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Criterio_2_R2 = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Criterio_3_R1 = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Criterio_3_R2 = table.Column<double>(type: "float", nullable: false),
                    Criterio_4_R1 = table.Column<double>(type: "float", nullable: false),
                    Criterio_4_R2 = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Resultado_R1 = table.Column<double>(type: "float", nullable: false),
                    Resultado_R2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyEvaluatorEvaluation1s", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetencyEvaluatorEvaluation1s");
        }
    }
}
