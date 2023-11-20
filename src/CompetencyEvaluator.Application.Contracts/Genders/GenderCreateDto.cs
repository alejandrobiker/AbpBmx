using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderCreateDtoBase
    {
        [Required]
        [StringLength(GenderConsts.nameMaxLength, MinimumLength = GenderConsts.nameMinLength)]
        public string name { get; set; } = null!;
        [StringLength(GenderConsts.ShortNameMaxLength)]
        public string? ShortName { get; set; }
    }
}