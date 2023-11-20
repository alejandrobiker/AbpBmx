using Localization.Resources.AbpUi;
using CompetencyEvaluator.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(CompetencyEvaluatorApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CompetencyEvaluatorHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CompetencyEvaluatorHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CompetencyEvaluatorResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
