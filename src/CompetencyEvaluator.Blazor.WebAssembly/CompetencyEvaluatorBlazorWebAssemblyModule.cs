using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator.Blazor.WebAssembly;

[DependsOn(
    typeof(CompetencyEvaluatorBlazorModule),
    typeof(CompetencyEvaluatorHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class CompetencyEvaluatorBlazorWebAssemblyModule : AbpModule
{

}
