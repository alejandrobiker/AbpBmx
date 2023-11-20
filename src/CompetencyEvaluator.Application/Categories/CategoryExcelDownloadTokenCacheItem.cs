using System;

namespace CompetencyEvaluator.Categories;

public abstract class CategoryExcelDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}