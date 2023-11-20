using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(AthleteConsts.NameMaxLength, MinimumLength = AthleteConsts.NameMinLength)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Guid GenderId { get; set; }
        public Guid CategoryId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}