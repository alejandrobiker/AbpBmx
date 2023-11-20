using CompetencyEvaluator.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompetencyEvaluator;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CompetencyEvaluatorEntityFrameworkCoreTestModule)
    )]
public class CompetencyEvaluatorDomainTestModule : AbpModule
{

}
