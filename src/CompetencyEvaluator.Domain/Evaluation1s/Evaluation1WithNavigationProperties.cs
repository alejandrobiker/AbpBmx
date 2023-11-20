using CompetencyEvaluator.Athletes;

using System;
using System.Collections.Generic;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1WithNavigationPropertiesBase
    {
        public Evaluation1 Evaluation1 { get; set; } = null!;

        public Athlete Athlete { get; set; } = null!;
        

        
    }
}