using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using CompetencyEvaluator.EntityFrameworkCore;

namespace CompetencyEvaluator.TypeRules
{
    public abstract class EfCoreTypeRuleRepositoryBase : EfCoreRepository<CompetencyEvaluatorDbContext, TypeRule, Guid>
    {
        public EfCoreTypeRuleRepositoryBase(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TypeRuleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
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