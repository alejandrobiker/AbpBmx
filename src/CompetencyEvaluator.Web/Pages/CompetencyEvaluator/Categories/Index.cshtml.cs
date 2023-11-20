using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Categories
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public int? MaxAgeFilterMin { get; set; }

        public int? MaxAgeFilterMax { get; set; }

        protected ICategoriesAppService _categoriesAppService;

        public IndexModelBase(ICategoriesAppService categoriesAppService)
        {
            _categoriesAppService = categoriesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}