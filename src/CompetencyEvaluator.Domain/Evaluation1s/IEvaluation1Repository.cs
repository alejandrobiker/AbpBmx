using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CompetencyEvaluator.Evaluation1s
{
    public partial interface IEvaluation1Repository : IRepository<Evaluation1, Guid>
    {
        Task<Evaluation1WithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<Evaluation1WithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            Guid? athleteId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Evaluation1>> GetListAsync(
                    string? filterText = null,
                    double? criterio_1_R1Min = null,
                    double? criterio_1_R1Max = null,
                    double? criterio_1_R2Min = null,
                    double? criterio_1_R2Max = null,
                    double? criterio_2_R1Min = null,
                    double? criterio_2_R1Max = null,
                    double? criterio_2_R2Min = null,
                    double? criterio_2_R2Max = null,
                    double? criterio_3_R1Min = null,
                    double? criterio_3_R1Max = null,
                    double? criterio_3_R2Min = null,
                    double? criterio_3_R2Max = null,
                    double? criterio_4_R1Min = null,
                    double? criterio_4_R1Max = null,
                    double? criterio_4_R2Min = null,
                    double? criterio_4_R2Max = null,
                    double? resultado_R1Min = null,
                    double? resultado_R1Max = null,
                    double? resultado_R2Min = null,
                    double? resultado_R2Max = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            double? criterio_1_R1Min = null,
            double? criterio_1_R1Max = null,
            double? criterio_1_R2Min = null,
            double? criterio_1_R2Max = null,
            double? criterio_2_R1Min = null,
            double? criterio_2_R1Max = null,
            double? criterio_2_R2Min = null,
            double? criterio_2_R2Max = null,
            double? criterio_3_R1Min = null,
            double? criterio_3_R1Max = null,
            double? criterio_3_R2Min = null,
            double? criterio_3_R2Max = null,
            double? criterio_4_R1Min = null,
            double? criterio_4_R1Max = null,
            double? criterio_4_R2Min = null,
            double? criterio_4_R2Max = null,
            double? resultado_R1Min = null,
            double? resultado_R1Max = null,
            double? resultado_R2Min = null,
            double? resultado_R2Max = null,
            Guid? athleteId = null,
            CancellationToken cancellationToken = default);
    }
}