using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator.Blazor.Server;

[DependsOn(
    typeof(CompetencyEvaluatorBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class CompetencyEvaluatorBlazorServerModule : AbpModule
{

}
