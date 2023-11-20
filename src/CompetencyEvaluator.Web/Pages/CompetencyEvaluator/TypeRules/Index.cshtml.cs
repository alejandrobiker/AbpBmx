using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.TypeRules;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? nameFilter { get; set; }

        protected ITypeRulesAppService _typeRulesAppService;

        public IndexModelBase(ITypeRulesAppService typeRulesAppService)
        {
            _typeRulesAppService = typeRulesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}