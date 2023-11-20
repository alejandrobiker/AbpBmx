using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CompetencyEvaluator.MongoDB;

public static class CompetencyEvaluatorMongoDbContextExtensions
{
    public static void ConfigureCompetencyEvaluator(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
