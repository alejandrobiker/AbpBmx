using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteCreateDtoBase
    {
        [Required]
        [StringLength(AthleteConsts.NameMaxLength, MinimumLength = AthleteConsts.NameMinLength)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Guid GenderId { get; set; }
        public Guid CategoryId { get; set; }
    }
}