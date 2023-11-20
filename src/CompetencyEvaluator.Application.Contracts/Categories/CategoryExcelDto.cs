using System;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryExcelDtoBase
    {
        public string Name { get; set; } = null!;
        public int? MaxAge { get; set; }
    }
}