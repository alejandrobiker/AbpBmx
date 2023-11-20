using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(CompetencyEvaluatorApplicationModule),
    typeof(CompetencyEvaluatorDomainTestModule)
    )]
public class CompetencyEvaluatorApplicationTestModule : AbpModule
{

}
