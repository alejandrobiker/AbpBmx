using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Evaluation1s;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public abstract class EditModalModelBase : CompetencyEvaluatorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public Evaluation1UpdateViewModel Evaluation1 { get; set; }

        public List<SelectListItem> AthleteLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IEvaluation1sAppService _evaluation1sAppService;

        public EditModalModelBase(IEvaluation1sAppService evaluation1sAppService)
        {
            _evaluation1sAppService = evaluation1sAppService;

            Evaluation1 = new();
        }

        public virtual async Task OnGetAsync()
        {
            var evaluation1WithNavigationPropertiesDto = await _evaluation1sAppService.GetWithNavigationPropertiesAsync(Id);
            Evaluation1 = ObjectMapper.Map<Evaluation1Dto, Evaluation1UpdateViewModel>(evaluation1WithNavigationPropertiesDto.Evaluation1);

            AthleteLookupListRequired.AddRange((
                                    await _evaluation1sAppService.GetAthleteLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _evaluation1sAppService.UpdateAsync(Id, ObjectMapper.Map<Evaluation1UpdateViewModel, Evaluation1UpdateDto>(Evaluation1));
            return NoContent();
        }
    }

    public class Evaluation1UpdateViewModel : Evaluation1UpdateDto
    {
    }
}