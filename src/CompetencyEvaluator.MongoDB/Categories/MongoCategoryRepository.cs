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

namespace CompetencyEvaluator.Categories
{
    public abstract class MongoCategoryRepositoryBase : MongoDbRepository<CompetencyEvaluatorMongoDbContext, Category, Guid>
    {
        public MongoCategoryRepositoryBase(IMongoDbContextProvider<CompetencyEvaluatorMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<Category>> GetListAsync(
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, maxAgeMin, maxAgeMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CategoryConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Category>>()
                .PageBy<Category, IMongoQueryable<Category>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name, maxAgeMin, maxAgeMax);
            return await query.As<IMongoQueryable<Category>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Category> ApplyFilter(
            IQueryable<Category> query,
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(maxAgeMin.HasValue, e => e.MaxAge >= maxAgeMin!.Value)
                    .WhereIf(maxAgeMax.HasValue, e => e.MaxAge <= maxAgeMax!.Value);
        }
    }
}