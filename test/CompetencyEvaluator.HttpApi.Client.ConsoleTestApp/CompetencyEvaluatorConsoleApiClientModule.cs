using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CompetencyEvaluatorHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CompetencyEvaluatorConsoleApiClientModule : AbpModule
{

}
