using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CompetencyEvaluator.Genders
{
    public partial interface IGenderRepository : IRepository<Gender, Guid>
    {
        Task<List<Gender>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? shortName = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? shortName = null,
            CancellationToken cancellationToken = default);
    }
}