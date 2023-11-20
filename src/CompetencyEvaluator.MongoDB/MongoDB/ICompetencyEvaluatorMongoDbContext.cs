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
public interface ICompetencyEvaluatorMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Evaluation1> Evaluation1s { get; }
    IMongoCollection<Athlete> Athletes { get; }
    IMongoCollection<Category> Categories { get; }
    IMongoCollection<Gender> Genders { get; }
    IMongoCollection<TypeRule> TypeRules { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}