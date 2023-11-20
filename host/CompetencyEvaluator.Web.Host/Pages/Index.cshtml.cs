using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CompetencyEvaluator.Pages;

public class IndexModel : CompetencyEvaluatorPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
