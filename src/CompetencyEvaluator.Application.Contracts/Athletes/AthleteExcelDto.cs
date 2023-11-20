using System;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteExcelDtoBase
    {
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}