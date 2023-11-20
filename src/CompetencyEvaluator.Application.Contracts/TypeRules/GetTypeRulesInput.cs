using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class GetTypeRulesInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? name { get; set; }

        public GetTypeRulesInputBase()
        {

        }
    }
}