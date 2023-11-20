using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public int? MaxAge { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}