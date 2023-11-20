using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Athletes;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Athletes
{
    public abstract class EditModalModelBase : CompetencyEvaluatorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AthleteUpdateViewModel Athlete { get; set; }

        public List<SelectListItem> GenderLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CategoryLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IAthletesAppService _athletesAppService;

        public EditModalModelBase(IAthletesAppService athletesAppService)
        {
            _athletesAppService = athletesAppService;

            Athlete = new();
        }

        public virtual async Task OnGetAsync()
        {
            var athleteWithNavigationPropertiesDto = await _athletesAppService.GetWithNavigationPropertiesAsync(Id);
            Athlete = ObjectMapper.Map<AthleteDto, AthleteUpdateViewModel>(athleteWithNavigationPropertiesDto.Athlete);

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

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _athletesAppService.UpdateAsync(Id, ObjectMapper.Map<AthleteUpdateViewModel, AthleteUpdateDto>(Athlete));
            return NoContent();
        }
    }

    public class AthleteUpdateViewModel : AthleteUpdateDto
    {
    }
}