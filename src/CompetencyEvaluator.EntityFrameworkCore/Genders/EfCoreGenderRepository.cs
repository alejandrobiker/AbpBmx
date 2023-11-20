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

namespace CompetencyEvaluator.Genders
{
    public abstract class EfCoreGenderRepositoryBase : EfCoreRepository<CompetencyEvaluatorDbContext, Gender, Guid>
    {
        public EfCoreGenderRepositoryBase(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, shortName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? GenderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? shortName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, shortName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
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