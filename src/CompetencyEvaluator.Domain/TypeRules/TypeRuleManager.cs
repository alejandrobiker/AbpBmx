using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class TypeRuleManagerBase : DomainService
    {
        protected ITypeRuleRepository _typeRuleRepository;

        public TypeRuleManagerBase(ITypeRuleRepository typeRuleRepository)
        {
            _typeRuleRepository = typeRuleRepository;
        }

        public virtual async Task<TypeRule> CreateAsync(
        string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), TypeRuleConsts.nameMaxLength, TypeRuleConsts.nameMinLength);

            var typeRule = new TypeRule(
             GuidGenerator.Create(),
             name
             );

            return await _typeRuleRepository.InsertAsync(typeRule);
        }

        public virtual async Task<TypeRule> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), TypeRuleConsts.nameMaxLength, TypeRuleConsts.nameMinLength);

            var typeRule = await _typeRuleRepository.GetAsync(id);

            typeRule.name = name;

            typeRule.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _typeRuleRepository.UpdateAsync(typeRule);
        }

    }
}