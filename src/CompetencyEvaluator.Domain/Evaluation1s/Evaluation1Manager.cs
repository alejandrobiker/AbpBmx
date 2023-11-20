using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1ManagerBase : DomainService
    {
        protected IEvaluation1Repository _evaluation1Repository;

        public Evaluation1ManagerBase(IEvaluation1Repository evaluation1Repository)
        {
            _evaluation1Repository = evaluation1Repository;
        }

        public virtual async Task<Evaluation1> CreateAsync(
        Guid athleteId, double criterio_1_R1, double criterio_1_R2, double criterio_2_R1, double criterio_2_R2, double criterio_3_R1, double criterio_3_R2, double criterio_4_R1, double criterio_4_R2, double resultado_R1, double resultado_R2)
        {
            Check.NotNull(athleteId, nameof(athleteId));
            Check.Range(criterio_1_R1, nameof(criterio_1_R1), Evaluation1Consts.Criterio_1_R1MinLength, Evaluation1Consts.Criterio_1_R1MaxLength);
            Check.Range(criterio_1_R2, nameof(criterio_1_R2), Evaluation1Consts.Criterio_1_R2MinLength, Evaluation1Consts.Criterio_1_R2MaxLength);
            Check.Range(criterio_2_R1, nameof(criterio_2_R1), Evaluation1Consts.Criterio_2_R1MinLength, Evaluation1Consts.Criterio_2_R1MaxLength);
            Check.Range(criterio_2_R2, nameof(criterio_2_R2), Evaluation1Consts.Criterio_2_R2MinLength, Evaluation1Consts.Criterio_2_R2MaxLength);
            Check.Range(criterio_3_R1, nameof(criterio_3_R1), Evaluation1Consts.Criterio_3_R1MinLength, Evaluation1Consts.Criterio_3_R1MaxLength);
            Check.Range(criterio_3_R2, nameof(criterio_3_R2), Evaluation1Consts.Criterio_3_R2MinLength, Evaluation1Consts.Criterio_3_R2MaxLength);
            Check.Range(criterio_4_R1, nameof(criterio_4_R1), Evaluation1Consts.Criterio_4_R1MinLength, Evaluation1Consts.Criterio_4_R1MaxLength);
            Check.Range(criterio_4_R2, nameof(criterio_4_R2), Evaluation1Consts.Criterio_4_R2MinLength, Evaluation1Consts.Criterio_4_R2MaxLength);

            var evaluation1 = new Evaluation1(
             GuidGenerator.Create(),
             athleteId, criterio_1_R1, criterio_1_R2, criterio_2_R1, criterio_2_R2, criterio_3_R1, criterio_3_R2, criterio_4_R1, criterio_4_R2, resultado_R1, resultado_R2
             );

            return await _evaluation1Repository.InsertAsync(evaluation1);
        }

        public virtual async Task<Evaluation1> UpdateAsync(
            Guid id,
            Guid athleteId, double criterio_1_R1, double criterio_1_R2, double criterio_2_R1, double criterio_2_R2, double criterio_3_R1, double criterio_3_R2, double criterio_4_R1, double criterio_4_R2, double resultado_R1, double resultado_R2, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(athleteId, nameof(athleteId));
            Check.Range(criterio_1_R1, nameof(criterio_1_R1), Evaluation1Consts.Criterio_1_R1MinLength, Evaluation1Consts.Criterio_1_R1MaxLength);
            Check.Range(criterio_1_R2, nameof(criterio_1_R2), Evaluation1Consts.Criterio_1_R2MinLength, Evaluation1Consts.Criterio_1_R2MaxLength);
            Check.Range(criterio_2_R1, nameof(criterio_2_R1), Evaluation1Consts.Criterio_2_R1MinLength, Evaluation1Consts.Criterio_2_R1MaxLength);
            Check.Range(criterio_2_R2, nameof(criterio_2_R2), Evaluation1Consts.Criterio_2_R2MinLength, Evaluation1Consts.Criterio_2_R2MaxLength);
            Check.Range(criterio_3_R1, nameof(criterio_3_R1), Evaluation1Consts.Criterio_3_R1MinLength, Evaluation1Consts.Criterio_3_R1MaxLength);
            Check.Range(criterio_3_R2, nameof(criterio_3_R2), Evaluation1Consts.Criterio_3_R2MinLength, Evaluation1Consts.Criterio_3_R2MaxLength);
            Check.Range(criterio_4_R1, nameof(criterio_4_R1), Evaluation1Consts.Criterio_4_R1MinLength, Evaluation1Consts.Criterio_4_R1MaxLength);
            Check.Range(criterio_4_R2, nameof(criterio_4_R2), Evaluation1Consts.Criterio_4_R2MinLength, Evaluation1Consts.Criterio_4_R2MaxLength);

            var evaluation1 = await _evaluation1Repository.GetAsync(id);

            evaluation1.AthleteId = athleteId;
            evaluation1.Criterio_1_R1 = criterio_1_R1;
            evaluation1.Criterio_1_R2 = criterio_1_R2;
            evaluation1.Criterio_2_R1 = criterio_2_R1;
            evaluation1.Criterio_2_R2 = criterio_2_R2;
            evaluation1.Criterio_3_R1 = criterio_3_R1;
            evaluation1.Criterio_3_R2 = criterio_3_R2;
            evaluation1.Criterio_4_R1 = criterio_4_R1;
            evaluation1.Criterio_4_R2 = criterio_4_R2;
            evaluation1.Resultado_R1 = resultado_R1;
            evaluation1.Resultado_R2 = resultado_R2;

            evaluation1.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _evaluation1Repository.UpdateAsync(evaluation1);
        }

    }
}