using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CompetencyEvaluator.Genders
{
    public abstract class GenderManagerBase : DomainService
    {
        protected IGenderRepository _genderRepository;

        public GenderManagerBase(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public virtual async Task<Gender> CreateAsync(
        string name, string? shortName = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), GenderConsts.nameMaxLength, GenderConsts.nameMinLength);
            Check.Length(shortName, nameof(shortName), GenderConsts.ShortNameMaxLength);

            var gender = new Gender(
             GuidGenerator.Create(),
             name, shortName
             );

            return await _genderRepository.InsertAsync(gender);
        }

        public virtual async Task<Gender> UpdateAsync(
            Guid id,
            string name, string? shortName = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), GenderConsts.nameMaxLength, GenderConsts.nameMinLength);
            Check.Length(shortName, nameof(shortName), GenderConsts.ShortNameMaxLength);

            var gender = await _genderRepository.GetAsync(id);

            gender.name = name;
            gender.ShortName = shortName;

            gender.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _genderRepository.UpdateAsync(gender);
        }

    }
}