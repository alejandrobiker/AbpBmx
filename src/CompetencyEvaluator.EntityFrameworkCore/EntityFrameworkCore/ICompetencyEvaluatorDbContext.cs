using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.TypeRules;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompetencyEvaluator.EntityFrameworkCore;

[ConnectionStringName(CompetencyEvaluatorDbProperties.ConnectionStringName)]
public interface ICompetencyEvaluatorDbContext : IEfCoreDbContext
{
    DbSet<Evaluation1> Evaluation1s { get; set; }
    DbSet<Athlete> Athletes { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Gender> Genders { get; set; }
    DbSet<TypeRule> TypeRules { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}