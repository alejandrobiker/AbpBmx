using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CompetencyEvaluator.Evaluation1s;
using CompetencyEvaluator.Shared;

namespace CompetencyEvaluator.Web.Pages.CompetencyEvaluator.Evaluation1s
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public double? Criterio_1_R1FilterMin { get; set; }

        public double? Criterio_1_R1FilterMax { get; set; }
        public double? Criterio_1_R2FilterMin { get; set; }

        public double? Criterio_1_R2FilterMax { get; set; }
        public double? Criterio_2_R1FilterMin { get; set; }

        public double? Criterio_2_R1FilterMax { get; set; }
        public double? Criterio_2_R2FilterMin { get; set; }

        public double? Criterio_2_R2FilterMax { get; set; }
        public double? Criterio_3_R1FilterMin { get; set; }

        public double? Criterio_3_R1FilterMax { get; set; }
        public double? Criterio_3_R2FilterMin { get; set; }

        public double? Criterio_3_R2FilterMax { get; set; }
        public double? Criterio_4_R1FilterMin { get; set; }

        public double? Criterio_4_R1FilterMax { get; set; }
        public double? Criterio_4_R2FilterMin { get; set; }

        public double? Criterio_4_R2FilterMax { get; set; }
        public double? Resultado_R1FilterMin { get; set; }

        public double? Resultado_R1FilterMax { get; set; }
        public double? Resultado_R2FilterMin { get; set; }

        public double? Resultado_R2FilterMax { get; set; }
        [SelectItems(nameof(AthleteLookupList))]
        public Guid AthleteIdFilter { get; set; }
        public List<SelectListItem> AthleteLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IEvaluation1sAppService _evaluation1sAppService;

        public IndexModelBase(IEvaluation1sAppService evaluation1sAppService)
        {
            _evaluation1sAppService = evaluation1sAppService;
        }

        public virtual async Task OnGetAsync()
        {
            AthleteLookupList.AddRange((
                    await _evaluation1sAppService.GetAthleteLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}