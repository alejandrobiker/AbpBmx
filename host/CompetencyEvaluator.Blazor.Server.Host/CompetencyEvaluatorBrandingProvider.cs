using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CompetencyEvaluator.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class CompetencyEvaluatorBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CompetencyEvaluator";
}
