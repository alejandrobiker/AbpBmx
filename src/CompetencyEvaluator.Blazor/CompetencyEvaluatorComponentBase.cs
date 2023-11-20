using CompetencyEvaluator.Localization;
using Volo.Abp.AspNetCore.Components;

namespace CompetencyEvaluator.Blazor;

public abstract class CompetencyEvaluatorComponentBase : AbpComponentBase
{
    protected CompetencyEvaluatorComponentBase()
    {
        LocalizationResource = typeof(CompetencyEvaluatorResource);
    }
}
