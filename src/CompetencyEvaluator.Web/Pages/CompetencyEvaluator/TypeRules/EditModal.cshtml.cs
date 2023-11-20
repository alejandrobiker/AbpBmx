using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.TypeRules;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.TypeRules
{
    public abstract class EditModalModelBase : CompetencyEvaluatorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public TypeRuleUpdateViewModel TypeRule { get; set; }

        protected ITypeRulesAppService _typeRulesAppService;

        public EditModalModelBase(ITypeRulesAppService typeRulesAppService)
        {
            _typeRulesAppService = typeRulesAppService;

            TypeRule = new();
        }

        public virtual async Task OnGetAsync()
        {
            var typeRule = await _typeRulesAppService.GetAsync(Id);
            TypeRule = ObjectMapper.Map<TypeRuleDto, TypeRuleUpdateViewModel>(typeRule);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _typeRulesAppService.UpdateAsync(Id, ObjectMapper.Map<TypeRuleUpdateViewModel, TypeRuleUpdateDto>(TypeRule));
            return NoContent();
        }
    }

    public class TypeRuleUpdateViewModel : TypeRuleUpdateDto
    {
    }
}