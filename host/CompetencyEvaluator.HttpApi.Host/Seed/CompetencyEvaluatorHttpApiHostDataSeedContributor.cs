using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace CompetencyEvaluator.Seed;

public class CompetencyEvaluatorHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly CompetencyEvaluatorSampleDataSeeder _competencyEvaluatorSampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public CompetencyEvaluatorHttpApiHostDataSeedContributor(
        CompetencyEvaluatorSampleDataSeeder competencyEvaluatorSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _competencyEvaluatorSampleDataSeeder = competencyEvaluatorSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _competencyEvaluatorSampleDataSeeder.SeedAsync(context!);
        }
    }
}
