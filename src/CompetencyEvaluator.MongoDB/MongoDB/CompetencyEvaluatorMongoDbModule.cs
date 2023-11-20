using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.TypeRules;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace CompetencyEvaluator.MongoDB;

[DependsOn(
    typeof(CompetencyEvaluatorDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class CompetencyEvaluatorMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CompetencyEvaluatorMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<TypeRule, TypeRules.MongoTypeRuleRepository>();

            options.AddRepository<Gender, Genders.MongoGenderRepository>();

            options.AddRepository<Category, Categories.MongoCategoryRepository>();

            options.AddRepository<Athlete, Athletes.MongoAthleteRepository>();

            options.AddRepository<Evaluation1, Evaluation1s.MongoEvaluation1Repository>();

        });
    }
}