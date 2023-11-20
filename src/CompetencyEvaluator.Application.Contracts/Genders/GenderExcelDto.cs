using System;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderExcelDtoBase
    {
        public string name { get; set; } = null!;
        public string? ShortName { get; set; }
    }
}