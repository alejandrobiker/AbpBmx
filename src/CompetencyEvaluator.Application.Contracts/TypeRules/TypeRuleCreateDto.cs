using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleCreateDtoBase
    {
        [Required]
        [StringLength(TypeRuleConsts.nameMaxLength, MinimumLength = TypeRuleConsts.nameMinLength)]
        public string name { get; set; } = null!;
    }
}