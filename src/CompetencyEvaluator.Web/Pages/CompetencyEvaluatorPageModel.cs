using CompetencyEvaluator.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CompetencyEvaluator.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class CompetencyEvaluatorPageModel : AbpPageModel
{
    protected CompetencyEvaluatorPageModel()
    {
        LocalizationResourceType = typeof(CompetencyEvaluatorResource);
        ObjectMapperContext = typeof(CompetencyEvaluatorWebModule);
    }
}
