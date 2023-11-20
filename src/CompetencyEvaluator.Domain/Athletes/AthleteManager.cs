using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CompetencyEvaluator.Athletes
{
    public abstract class AthleteManagerBase : DomainService
    {
        protected IAthleteRepository _athleteRepository;

        public AthleteManagerBase(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public virtual async Task<Athlete> CreateAsync(
        Guid genderId, Guid categoryId, string name, DateTime dateOfBirth)
        {
            Check.NotNull(genderId, nameof(genderId));
            Check.NotNull(categoryId, nameof(categoryId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), AthleteConsts.NameMaxLength, AthleteConsts.NameMinLength);
            Check.NotNull(dateOfBirth, nameof(dateOfBirth));

            var athlete = new Athlete(
             GuidGenerator.Create(),
             genderId, categoryId, name, dateOfBirth
             );

            return await _athleteRepository.InsertAsync(athlete);
        }

        public virtual async Task<Athlete> UpdateAsync(
            Guid id,
            Guid genderId, Guid categoryId, string name, DateTime dateOfBirth, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(genderId, nameof(genderId));
            Check.NotNull(categoryId, nameof(categoryId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), AthleteConsts.NameMaxLength, AthleteConsts.NameMinLength);
            Check.NotNull(dateOfBirth, nameof(dateOfBirth));

            var athlete = await _athleteRepository.GetAsync(id);

            athlete.GenderId = genderId;
            athlete.CategoryId = categoryId;
            athlete.Name = name;
            athlete.DateOfBirth = dateOfBirth;

            athlete.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _athleteRepository.UpdateAsync(athlete);
        }

    }
}