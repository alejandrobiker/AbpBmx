using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Genders
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? nameFilter { get; set; }
        public string? ShortNameFilter { get; set; }

        protected IGendersAppService _gendersAppService;

        public IndexModelBase(IGendersAppService gendersAppService)
        {
            _gendersAppService = gendersAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}