using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.Categories;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Categories
{
    public abstract class CreateModalModelBase : CompetencyEvaluatorPageModel
    {
        [BindProperty]
        public CategoryCreateViewModel Category { get; set; }

        protected ICategoriesAppService _categoriesAppService;

        public CreateModalModelBase(ICategoriesAppService categoriesAppService)
        {
            _categoriesAppService = categoriesAppService;

            Category = new();
        }

        public virtual async Task OnGetAsync()
        {
            Category = new CategoryCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _categoriesAppService.CreateAsync(ObjectMapper.Map<CategoryCreateViewModel, CategoryCreateDto>(Category));
            return NoContent();
        }
    }

    public class CategoryCreateViewModel : CategoryCreateDto
    {
    }
}