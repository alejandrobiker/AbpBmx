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

namespace CompetencyEvaluator.TypeRules
{
    public abstract class MongoTypeRuleRepositoryBase : MongoDbRepository<CompetencyEvaluatorMongoDbContext, TypeRule, Guid>
    {
        public MongoTypeRuleRepositoryBase(IMongoDbContextProvider<CompetencyEvaluatorMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<TypeRule>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TypeRuleConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<TypeRule>>()
                .PageBy<TypeRule, IMongoQueryable<TypeRule>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, name);
            return await query.As<IMongoQueryable<TypeRule>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TypeRule> ApplyFilter(
            IQueryable<TypeRule> query,
            string? filterText = null,
            string? name = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.name.Contains(name));
        }
    }
}