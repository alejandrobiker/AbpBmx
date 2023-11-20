using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.Athletes;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Athletes
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public DateTime? DateOfBirthFilterMin { get; set; }

        public DateTime? DateOfBirthFilterMax { get; set; }
        [SelectItems(nameof(GenderLookupList))]
        public Guid GenderIdFilter { get; set; }
        public List<SelectListItem> GenderLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CategoryLookupList))]
        public Guid CategoryIdFilter { get; set; }
        public List<SelectListItem> CategoryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IAthletesAppService _athletesAppService;

        public IndexModelBase(IAthletesAppService athletesAppService)
        {
            _athletesAppService = athletesAppService;
        }

        public virtual async Task OnGetAsync()
        {
            GenderLookupList.AddRange((
                    await _athletesAppService.GetGenderLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            CategoryLookupList.AddRange((
                            await _athletesAppService.GetCategoryLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}