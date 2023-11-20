using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string name { get; set; }

        protected TypeRuleBase()
        {

        }

        public TypeRuleBase(Guid id, string name)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), TypeRuleConsts.nameMaxLength, TypeRuleConsts.nameMinLength);
            this.name = name;
        }

    }
}