using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CompetencyEvaluator.Categories
{
    public partial interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<List<Category>> GetListAsync(
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? maxAgeMin = null,
            int? maxAgeMax = null,
            CancellationToken cancellationToken = default);
    }
}