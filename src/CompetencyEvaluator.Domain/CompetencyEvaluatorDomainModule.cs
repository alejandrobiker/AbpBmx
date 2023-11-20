using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(CompetencyEvaluatorDomainSharedModule)
)]
public class CompetencyEvaluatorDomainModule : AbpModule
{

}
