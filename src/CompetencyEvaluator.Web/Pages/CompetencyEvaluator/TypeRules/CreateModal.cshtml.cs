using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.TypeRules;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules
{
    public abstract class CreateModalModelBase : CompetencyEvaluatorPageModel
    {
        [BindProperty]
        public TypeRuleCreateViewModel TypeRule { get; set; }

        protected ITypeRulesAppService _typeRulesAppService;

        public CreateModalModelBase(ITypeRulesAppService typeRulesAppService)
        {
            _typeRulesAppService = typeRulesAppService;

            TypeRule = new();
        }

        public virtual async Task OnGetAsync()
        {
            TypeRule = new TypeRuleCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _typeRulesAppService.CreateAsync(ObjectMapper.Map<TypeRuleCreateViewModel, TypeRuleCreateDto>(TypeRule));
            return NoContent();
        }
    }

    public class TypeRuleCreateViewModel : TypeRuleCreateDto
    {
    }
}