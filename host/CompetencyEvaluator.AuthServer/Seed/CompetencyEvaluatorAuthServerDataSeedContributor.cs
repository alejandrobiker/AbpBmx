using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace CompetencyEvaluator.Seed;

public class CompetencyEvaluatorAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly CompetencyEvaluatorSampleIdentityDataSeeder _competencyEvaluatorSampleIdentityDataSeeder;
    private readonly CompetencyEvaluatorAuthServerDataSeeder _competencyEvaluatorAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public CompetencyEvaluatorAuthServerDataSeedContributor(
        CompetencyEvaluatorAuthServerDataSeeder competencyEvaluatorAuthServerDataSeeder,
        CompetencyEvaluatorSampleIdentityDataSeeder competencyEvaluatorSampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _competencyEvaluatorAuthServerDataSeeder = competencyEvaluatorAuthServerDataSeeder;
        _competencyEvaluatorSampleIdentityDataSeeder = competencyEvaluatorSampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _competencyEvaluatorSampleIdentityDataSeeder.SeedAsync(context!);
            await _competencyEvaluatorAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
