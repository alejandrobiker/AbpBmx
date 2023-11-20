using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Categories;

using System;
using System.Collections.Generic;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteWithNavigationPropertiesBase
    {
        public Athlete Athlete { get; set; } = null!;

        public Gender Gender { get; set; } = null!;
        public Category Category { get; set; } = null!;
        

        
    }
}