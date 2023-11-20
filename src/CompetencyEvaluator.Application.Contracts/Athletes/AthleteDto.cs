using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Guid GenderId { get; set; }
        public Guid CategoryId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}