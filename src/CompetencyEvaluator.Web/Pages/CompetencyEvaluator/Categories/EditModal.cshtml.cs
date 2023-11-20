using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Categories;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Categories
{
    public abstract class EditModalModelBase : CompetencyEvaluatorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CategoryUpdateViewModel Category { get; set; }

        protected ICategoriesAppService _categoriesAppService;

        public EditModalModelBase(ICategoriesAppService categoriesAppService)
        {
            _categoriesAppService = categoriesAppService;

            Category = new();
        }

        public virtual async Task OnGetAsync()
        {
            var category = await _categoriesAppService.GetAsync(Id);
            Category = ObjectMapper.Map<CategoryDto, CategoryUpdateViewModel>(category);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _categoriesAppService.UpdateAsync(Id, ObjectMapper.Map<CategoryUpdateViewModel, CategoryUpdateDto>(Category));
            return NoContent();
        }
    }

    public class CategoryUpdateViewModel : CategoryUpdateDto
    {
    }
}