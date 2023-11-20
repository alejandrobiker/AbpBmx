using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(GenderConsts.nameMaxLength, MinimumLength = GenderConsts.nameMinLength)]
        public string name { get; set; } = null!;
        [StringLength(GenderConsts.ShortNameMaxLength)]
        public string? ShortName { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}