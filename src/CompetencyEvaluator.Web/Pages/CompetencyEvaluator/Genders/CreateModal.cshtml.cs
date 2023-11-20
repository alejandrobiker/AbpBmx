using CompetencyEvaluator.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompetencyEvaluator.Genders;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Genders
{
    public abstract class CreateModalModelBase : CompetencyEvaluatorPageModel
    {
        [BindProperty]
        public GenderCreateViewModel Gender { get; set; }

        protected IGendersAppService _gendersAppService;

        public CreateModalModelBase(IGendersAppService gendersAppService)
        {
            _gendersAppService = gendersAppService;

            Gender = new();
        }

        public virtual async Task OnGetAsync()
        {
            Gender = new GenderCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _gendersAppService.CreateAsync(ObjectMapper.Map<GenderCreateViewModel, GenderCreateDto>(Gender));
            return NoContent();
        }
    }

    public class GenderCreateViewModel : GenderCreateDto
    {
    }
}