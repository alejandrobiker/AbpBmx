using CompetencyEvaluator.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CompetencyEvaluator.Pages;

public abstract class CompetencyEvaluatorPageModel : AbpPageModel
{
    protected CompetencyEvaluatorPageModel()
    {
        LocalizationResourceType = typeof(CompetencyEvaluatorResource);
    }
}
