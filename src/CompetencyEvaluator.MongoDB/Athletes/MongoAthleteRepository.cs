using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using CompetencyEvaluator.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace CompetencyEvaluator.Athletes
{
    public abstract class MongoAthleteRepositoryBase : MongoDbRepository<CompetencyEvaluatorMongoDbContext, Athlete, Guid>
    {
        public MongoAthleteRepositoryBase(IMongoDbContextProvider<CompetencyEvaluatorMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<AthleteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var athlete = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var gender = await (await GetMongoQueryableAsync<Gender>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == athlete.GenderId, cancellationToken: cancellationToken);
            var category = await (await GetMongoQueryableAsync<Category>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == athlete.CategoryId, cancellationToken: cancellationToken);

            return new AthleteWithNavigationProperties
            {
                Athlete = athlete,
                Gender = gender,
                Category = category,

            };
        }

        public virtual async Task<List<AthleteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, dateOfBirthMin, dateOfBirthMax, genderId, categoryId);
            var athletes = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AthleteConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<Athlete>>()
                .PageBy<Athlete, IMongoQueryable<Athlete>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return athletes.Select(s => new AthleteWithNavigationProperties
            {
                Athlete = s,
                Gender = ApplyDataFilters<IMongoQueryable<Gender>, Gender>(dbContext.Collection<Gender>().AsQueryable()).FirstOrDefault(e => e.Id == s.GenderId),
                Category = ApplyDataFilters<IMongoQueryable<Category>, Category>(dbContext.Collection<Category>().AsQueryable()).FirstOrDefault(e => e.Id == s.CategoryId),

            }).ToList();
        }

        public virtual async Task<List<Athlete>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, dateOfBirthMin, dateOfBirthMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AthleteConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Athlete>>()
                .PageBy<Athlete, IMongoQueryable<Athlete>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, dateOfBirthMin, dateOfBirthMax, genderId, categoryId);
            return await query.As<IMongoQueryable<Athlete>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Athlete> ApplyFilter(
            IQueryable<Athlete> query,
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(dateOfBirthMin.HasValue, e => e.DateOfBirth >= dateOfBirthMin!.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.DateOfBirth <= dateOfBirthMax!.Value)
                    .WhereIf(genderId != null && genderId != Guid.Empty, e => e.GenderId == genderId)
                    .WhereIf(categoryId != null && categoryId != Guid.Empty, e => e.CategoryId == categoryId);
        }
    }
}