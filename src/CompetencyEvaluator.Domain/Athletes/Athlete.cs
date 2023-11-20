using CompetencyEvaluator.Genders;
using CompetencyEvaluator.Categories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual DateTime DateOfBirth { get; set; }
        public Guid GenderId { get; set; }
        public Guid CategoryId { get; set; }

        protected AthleteBase()
        {

        }

        public AthleteBase(Guid id, Guid genderId, Guid categoryId, string name, DateTime dateOfBirth)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), AthleteConsts.NameMaxLength, AthleteConsts.NameMinLength);
            Name = name;
            DateOfBirth = dateOfBirth;
            GenderId = genderId;
            CategoryId = categoryId;
        }

    }
}