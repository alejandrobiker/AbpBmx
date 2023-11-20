using CompetencyEvaluator.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompetencyEvaluator;

public abstract class CompetencyEvaluatorController : AbpControllerBase
{
    protected CompetencyEvaluatorController()
    {
        LocalizationResource = typeof(CompetencyEvaluatorResource);
    }
}
