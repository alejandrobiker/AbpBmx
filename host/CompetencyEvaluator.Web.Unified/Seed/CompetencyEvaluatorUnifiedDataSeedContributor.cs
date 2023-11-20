using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace CompetencyEvaluator.Seed;

public class CompetencyEvaluatorUnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly CompetencyEvaluatorSampleIdentityDataSeeder _sampleIdentityDataSeeder;
    private readonly CompetencyEvaluatorSampleDataSeeder _competencyEvaluatorSampleDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ICurrentTenant _currentTenant;

    public CompetencyEvaluatorUnifiedDataSeedContributor(
        CompetencyEvaluatorSampleIdentityDataSeeder sampleIdentityDataSeeder,
        IUnitOfWorkManager unitOfWorkManager,
        CompetencyEvaluatorSampleDataSeeder competencyEvaluatorSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _sampleIdentityDataSeeder = sampleIdentityDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
        _competencyEvaluatorSampleDataSeeder = competencyEvaluatorSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _unitOfWorkManager.Current!.SaveChangesAsync();

        using (_currentTenant.Change(context.TenantId))
        {
            await _sampleIdentityDataSeeder.SeedAsync(context);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            await _competencyEvaluatorSampleDataSeeder.SeedAsync(context);
        }
    }
}
