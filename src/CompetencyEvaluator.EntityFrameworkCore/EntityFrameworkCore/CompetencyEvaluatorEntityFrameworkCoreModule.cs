using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.TypeRules;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator.EntityFrameworkCore;

[DependsOn(
    typeof(CompetencyEvaluatorDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CompetencyEvaluatorEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CompetencyEvaluatorDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<TypeRule, TypeRules.EfCoreTypeRuleRepository>();

            options.AddRepository<Gender, Genders.EfCoreGenderRepository>();

            options.AddRepository<Category, Categories.EfCoreCategoryRepository>();

            options.AddRepository<Athlete, Athletes.EfCoreAthleteRepository>();

            options.AddRepository<Evaluation1, Evaluation1s.EfCoreEvaluation1Repository>();

        });
    }
}