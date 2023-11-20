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

namespace CompetencyEvaluator.Genders
{
    public abstract class MongoGenderRepositoryBase : MongoDbRepository<CompetencyEvaluatorMongoDbContext, Gender, Guid>
    {
        public MongoGenderRepositoryBase(IMongoDbContextProvider<CompetencyEvaluatorMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<Gender>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? shortName = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, shortName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? GenderConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Gender>>()
                .PageBy<Gender, IMongoQueryable<Gender>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? shortName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, shortName);
            return await query.As<IMongoQueryable<Gender>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Gender> ApplyFilter(
            IQueryable<Gender> query,
            string? filterText = null,
            string? name = null,
            string? shortName = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.name!.Contains(filterText!) || e.ShortName!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ShortName.Contains(shortName));
        }
    }
}