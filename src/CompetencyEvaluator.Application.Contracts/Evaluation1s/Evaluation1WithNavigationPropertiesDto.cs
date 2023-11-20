using CompetencyEvaluator.Athletes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1WithNavigationPropertiesDtoBase
    {
        public Evaluation1Dto Evaluation1 { get; set; } = null!;

        public AthleteDto Athlete { get; set; } = null!;

    }
}