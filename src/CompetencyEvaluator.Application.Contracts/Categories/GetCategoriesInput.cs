using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.Categories
{
    public abstract class GetCategoriesInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public int? MaxAgeMin { get; set; }
        public int? MaxAgeMax { get; set; }

        public GetCategoriesInputBase()
        {

        }
    }
}