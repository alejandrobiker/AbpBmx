using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string name { get; set; }

        [CanBeNull]
        public virtual string? ShortName { get; set; }

        protected GenderBase()
        {

        }

        public GenderBase(Guid id, string name, string? shortName = null)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), GenderConsts.nameMaxLength, GenderConsts.nameMinLength);
            Check.Length(shortName, nameof(shortName), GenderConsts.ShortNameMaxLength, 0);
            this.name = name;
            ShortName = shortName;
        }

    }
}