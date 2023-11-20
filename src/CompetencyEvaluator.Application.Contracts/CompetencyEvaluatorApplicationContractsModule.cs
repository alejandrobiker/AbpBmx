using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(CompetencyEvaluatorDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class CompetencyEvaluatorApplicationContractsModule : AbpModule
{

}
