using CompetencyEvaluator.Categories;
using CompetencyEvaluator.Genders;
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

namespace CompetencyEvaluator.Athletes
{
    public abstract class EfCoreAthleteRepositoryBase : EfCoreRepository<CompetencyEvaluatorDbContext, Athlete, Guid>
    {
        public EfCoreAthleteRepositoryBase(IDbContextProvider<CompetencyEvaluatorDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<AthleteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(athlete => new AthleteWithNavigationProperties
                {
                    Athlete = athlete,
                    Gender = dbContext.Set<Gender>().FirstOrDefault(c => c.Id == athlete.GenderId),
                    Category = dbContext.Set<Category>().FirstOrDefault(c => c.Id == athlete.CategoryId)
                }).FirstOrDefault();
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, dateOfBirthMin, dateOfBirthMax, genderId, categoryId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AthleteConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<AthleteWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from athlete in (await GetDbSetAsync())
                   join gender in (await GetDbContextAsync()).Set<Gender>() on athlete.GenderId equals gender.Id into genders
                   from gender in genders.DefaultIfEmpty()
                   join category in (await GetDbContextAsync()).Set<Category>() on athlete.CategoryId equals category.Id into categories
                   from category in categories.DefaultIfEmpty()
                   select new AthleteWithNavigationProperties
                   {
                       Athlete = athlete,
                       Gender = gender,
                       Category = category
                   };
        }

        protected virtual IQueryable<AthleteWithNavigationProperties> ApplyFilter(
            IQueryable<AthleteWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Athlete.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Athlete.Name.Contains(name))
                    .WhereIf(dateOfBirthMin.HasValue, e => e.Athlete.DateOfBirth >= dateOfBirthMin!.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.Athlete.DateOfBirth <= dateOfBirthMax!.Value)
                    .WhereIf(genderId != null && genderId != Guid.Empty, e => e.Gender != null && e.Gender.Id == genderId)
                    .WhereIf(categoryId != null && categoryId != Guid.Empty, e => e.Category != null && e.Category.Id == categoryId);
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, dateOfBirthMin, dateOfBirthMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AthleteConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, dateOfBirthMin, dateOfBirthMax, genderId, categoryId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Athlete> ApplyFilter(
            IQueryable<Athlete> query,
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(dateOfBirthMin.HasValue, e => e.DateOfBirth >= dateOfBirthMin!.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.DateOfBirth <= dateOfBirthMax!.Value);
        }
    }
}