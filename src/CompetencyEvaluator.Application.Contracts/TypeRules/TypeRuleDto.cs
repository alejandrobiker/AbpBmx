using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}