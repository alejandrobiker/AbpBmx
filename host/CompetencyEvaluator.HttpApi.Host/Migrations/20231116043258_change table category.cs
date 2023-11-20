using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyEvaluator.Migrations
{
    /// <inheritdoc />
    public partial class changetablecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxAge",
                table: "CompetencyEvaluatorCategories",
                type: "int",
                maxLength: 99,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxAge",
                table: "CompetencyEvaluatorCategories",
                type: "int",
                maxLength: 2,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 99,
                oldNullable: true);
        }
    }
}
