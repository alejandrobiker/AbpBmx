using CompetencyEvaluator.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CompetencyEvaluator.Genders;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Genders
{
    public abstract class EditModalModelBase : CompetencyEvaluatorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public GenderUpdateViewModel Gender { get; set; }

        protected IGendersAppService _gendersAppService;

        public EditModalModelBase(IGendersAppService gendersAppService)
        {
            _gendersAppService = gendersAppService;

            Gender = new();
        }

        public virtual async Task OnGetAsync()
        {
            var gender = await _gendersAppService.GetAsync(Id);
            Gender = ObjectMapper.Map<GenderDto, GenderUpdateViewModel>(gender);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _gendersAppService.UpdateAsync(Id, ObjectMapper.Map<GenderUpdateViewModel, GenderUpdateDto>(Gender));
            return NoContent();
        }
    }

    public class GenderUpdateViewModel : GenderUpdateDto
    {
    }
}