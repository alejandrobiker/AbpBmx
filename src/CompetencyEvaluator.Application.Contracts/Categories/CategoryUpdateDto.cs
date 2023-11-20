using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CategoryConsts.NameMaxLength, MinimumLength = CategoryConsts.NameMinLength)]
        public string Name { get; set; } = null!;
        [Range(CategoryConsts.MaxAgeMinLength, CategoryConsts.MaxAgeMaxLength)]
        public int? MaxAge { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}