using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Categories;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteWithNavigationPropertiesDtoBase
    {
        public AthleteDto Athlete { get; set; } = null!;

        public GenderDto Gender { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;

    }
}