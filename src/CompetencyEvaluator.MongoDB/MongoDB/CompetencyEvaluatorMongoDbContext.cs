using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.TypeRules;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CompetencyEvaluator.MongoDB;

[ConnectionStringName(CompetencyEvaluatorDbProperties.ConnectionStringName)]
public class CompetencyEvaluatorMongoDbContext : AbpMongoDbContext, ICompetencyEvaluatorMongoDbContext
{
    public IMongoCollection<Evaluation1> Evaluation1s => Collection<Evaluation1>();
    public IMongoCollection<Athlete> Athletes => Collection<Athlete>();
    public IMongoCollection<Category> Categories => Collection<Category>();
    public IMongoCollection<Gender> Genders => Collection<Gender>();
    public IMongoCollection<TypeRule> TypeRules => Collection<TypeRule>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCompetencyEvaluator();

        modelBuilder.Entity<TypeRule>(b => { b.CollectionName = CompetencyEvaluatorDbProperties.DbTablePrefix + "TypeRules"; });

        modelBuilder.Entity<Gender>(b => { b.CollectionName = CompetencyEvaluatorDbProperties.DbTablePrefix + "Genders"; });

        modelBuilder.Entity<Category>(b => { b.CollectionName = CompetencyEvaluatorDbProperties.DbTablePrefix + "Categories"; });

        modelBuilder.Entity<Athlete>(b => { b.CollectionName = CompetencyEvaluatorDbProperties.DbTablePrefix + "Athletes"; });

        modelBuilder.Entity<Evaluation1>(b => { b.CollectionName = CompetencyEvaluatorDbProperties.DbTablePrefix + "Evaluation1s"; });
    }
}