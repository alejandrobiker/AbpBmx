using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryCreateDtoBase
    {
        [Required]
        [StringLength(CategoryConsts.NameMaxLength, MinimumLength = CategoryConsts.NameMinLength)]
        public string Name { get; set; } = null!;
        [Range(CategoryConsts.MaxAgeMinLength, CategoryConsts.MaxAgeMaxLength)]
        public int? MaxAge { get; set; }
    }
}