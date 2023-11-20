using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public class IndexModel : IndexModelBase
    {
        public IndexModel(IEvaluation1sAppService evaluation1sAppService)
            : base(evaluation1sAppService)
        {
        }
    }
}