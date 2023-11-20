using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(CompetencyEvaluatorDomainModule),
    typeof(CompetencyEvaluatorApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class CompetencyEvaluatorApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CompetencyEvaluatorApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CompetencyEvaluatorApplicationModule>(validate: true);
        });
    }
}
