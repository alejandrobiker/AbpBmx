using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.Evaluation1s;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public abstract class CreateModalModelBase : CompetencyEvaluatorPageModel
    {
        [BindProperty]
        public Evaluation1CreateViewModel Evaluation1 { get; set; }

        public List<SelectListItem> AthleteLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IEvaluation1sAppService _evaluation1sAppService;

        public CreateModalModelBase(IEvaluation1sAppService evaluation1sAppService)
        {
            _evaluation1sAppService = evaluation1sAppService;

            Evaluation1 = new();
        }

        public virtual async Task OnGetAsync()
        {
            Evaluation1 = new Evaluation1CreateViewModel();
            AthleteLookupListRequired.AddRange((
                                    await _evaluation1sAppService.GetAthleteLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _evaluation1sAppService.CreateAsync(ObjectMapper.Map<Evaluation1CreateViewModel, Evaluation1CreateDto>(Evaluation1));
            return NoContent();
        }
    }

    public class Evaluation1CreateViewModel : Evaluation1CreateDto
    {
    }
}