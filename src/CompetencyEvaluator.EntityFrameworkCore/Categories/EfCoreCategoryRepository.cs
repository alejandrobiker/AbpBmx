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

namespace CompetencyEvaluator.Categories
{
    public abstract class EfCoreCategoryRepositoryBase : EfCoreRepository<CompetencyEvaluatorDbContext, Category, Guid>
    {
        public EfCoreCategoryRepositoryBase(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, maxAgeMin, maxAgeMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CategoryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, maxAgeMin, maxAgeMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
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