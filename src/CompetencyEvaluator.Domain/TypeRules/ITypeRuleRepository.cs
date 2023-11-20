using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CompetencyEvaluator.TypeRules
{
    public partial interface ITypeRuleRepository : IRepository<TypeRule, Guid>
    {
        Task<List<TypeRule>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            CancellationToken cancellationToken = default);
    }
}