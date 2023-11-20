using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CompetencyEvaluator.Athletes
{
    public partial interface IAthleteRepository : IRepository<Athlete, Guid>
    {
        Task<AthleteWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<AthleteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Athlete>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    DateTime? dateOfBirthMin = null,
                    DateTime? dateOfBirthMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            Guid? genderId = null,
            Guid? categoryId = null,
            CancellationToken cancellationToken = default);
    }
}