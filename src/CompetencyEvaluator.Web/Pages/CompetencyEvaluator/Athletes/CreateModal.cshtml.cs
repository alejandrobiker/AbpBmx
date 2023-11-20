using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.Athletes;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Athletes
{
    public abstract class CreateModalModelBase : CompetencyEvaluatorPageModel
    {
        [BindProperty]
        public AthleteCreateViewModel Athlete { get; set; }

        public List<SelectListItem> GenderLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CategoryLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IAthletesAppService _athletesAppService;

        public CreateModalModelBase(IAthletesAppService athletesAppService)
        {
            _athletesAppService = athletesAppService;

            Athlete = new();
        }

        public virtual async Task OnGetAsync()
        {
            Athlete = new AthleteCreateViewModel();
            GenderLookupListRequired.AddRange((
                                    await _athletesAppService.GetGenderLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CategoryLookupListRequired.AddRange((
                                    await _athletesAppService.GetCategoryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _athletesAppService.CreateAsync(ObjectMapper.Map<AthleteCreateViewModel, AthleteCreateDto>(Athlete));
            return NoContent();
        }
    }

    public class AthleteCreateViewModel : AthleteCreateDto
    {
    }
}