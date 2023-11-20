using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.Genders
{
    public abstract class GetGendersInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? name { get; set; }
        public string? ShortName { get; set; }

        public GetGendersInputBase()
        {

        }
    }
}