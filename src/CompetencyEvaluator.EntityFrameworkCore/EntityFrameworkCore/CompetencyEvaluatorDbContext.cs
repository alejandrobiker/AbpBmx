using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.TypeRules;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompetencyEvaluator.EntityFrameworkCore;

[ConnectionStringName(CompetencyEvaluatorDbProperties.ConnectionStringName)]
public class CompetencyEvaluatorDbContext : AbpDbContext<CompetencyEvaluatorDbContext>, ICompetencyEvaluatorDbContext
{
    public DbSet<Evaluation1> Evaluation1s { get; set; } = null!;
    public DbSet<Athlete> Athletes { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<TypeRule> TypeRules { get; set; } = null!;
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CompetencyEvaluatorDbContext(DbContextOptions<CompetencyEvaluatorDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCompetencyEvaluator();
    }
}