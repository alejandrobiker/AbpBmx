using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(TypeRuleConsts.nameMaxLength, MinimumLength = TypeRuleConsts.nameMinLength)]
        public string name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}