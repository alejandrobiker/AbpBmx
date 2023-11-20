using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string name { get; set; } = null!;
        public string? ShortName { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}