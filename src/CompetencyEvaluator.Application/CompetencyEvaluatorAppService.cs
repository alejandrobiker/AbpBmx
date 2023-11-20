using CompetencyEvaluator.Localization;
using Volo.Abp.Application.Services;

namespace CompetencyEvaluator;

public abstract class CompetencyEvaluatorAppService : ApplicationService
{
    protected CompetencyEvaluatorAppService()
    {
        LocalizationResource = typeof(CompetencyEvaluatorResource);
        ObjectMapperContext = typeof(CompetencyEvaluatorApplicationModule);
    }
}
