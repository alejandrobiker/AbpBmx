using Microsoft.Extensions.DependencyInjection;
using CompetencyEvaluator.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace CompetencyEvaluator.Blazor;

[DependsOn(
    typeof(CompetencyEvaluatorApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class CompetencyEvaluatorBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CompetencyEvaluatorBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<CompetencyEvaluatorBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CompetencyEvaluatorMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(CompetencyEvaluatorBlazorModule).Assembly);
        });
    }
}
