using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace CompetencyEvaluator.Seed;

/* You can use this file to seed some sample data
 * to test your module easier.
 *
 * This class is shared among these projects:
 * - CompetencyEvaluator.AuthServer
 * - CompetencyEvaluator.Web.Unified (used as linked file)
 */
public class CompetencyEvaluatorSampleDataSeeder : ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {

    }
}
