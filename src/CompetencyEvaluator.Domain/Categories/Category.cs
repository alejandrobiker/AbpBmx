using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Categories
{
    public abstract class CategoryBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual int? MaxAge { get; set; }

        protected CategoryBase()
        {

        }

        public CategoryBase(Guid id, string name, int? maxAge = null)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), CategoryConsts.NameMaxLength, CategoryConsts.NameMinLength);
            if (maxAge < CategoryConsts.MaxAgeMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAge), maxAge, "The value of 'maxAge' cannot be lower than " + CategoryConsts.MaxAgeMinLength);
            }

            if (maxAge > CategoryConsts.MaxAgeMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAge), maxAge, "The value of 'maxAge' cannot be greater than " + CategoryConsts.MaxAgeMaxLength);
            }

            Name = name;
            MaxAge = maxAge;
        }

    }
}