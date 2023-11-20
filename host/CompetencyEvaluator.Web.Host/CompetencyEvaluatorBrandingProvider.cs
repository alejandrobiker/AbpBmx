using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CompetencyEvaluator;

[Dependency(ReplaceServices = true)]
public class CompetencyEvaluatorBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CompetencyEvaluator";
}
