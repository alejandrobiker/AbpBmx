using CompetencyEvaluator.Athletes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1Base : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual double Criterio_1_R1 { get; set; }

        public virtual double Criterio_1_R2 { get; set; }

        public virtual double Criterio_2_R1 { get; set; }

        public virtual double Criterio_2_R2 { get; set; }

        public virtual double Criterio_3_R1 { get; set; }

        public virtual double Criterio_3_R2 { get; set; }

        public virtual double Criterio_4_R1 { get; set; }

        public virtual double Criterio_4_R2 { get; set; }

        public virtual double Resultado_R1 { get; set; }

        public virtual double Resultado_R2 { get; set; }
        public Guid AthleteId { get; set; }

        protected Evaluation1Base()
        {

        }

        public Evaluation1Base(Guid id, Guid athleteId, double criterio_1_R1, double criterio_1_R2, double criterio_2_R1, double criterio_2_R2, double criterio_3_R1, double criterio_3_R2, double criterio_4_R1, double criterio_4_R2, double resultado_R1, double resultado_R2)
        {

            Id = id;
            if (criterio_1_R1 < Evaluation1Consts.Criterio_1_R1MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_1_R1), criterio_1_R1, "The value of 'criterio_1_R1' cannot be lower than " + Evaluation1Consts.Criterio_1_R1MinLength);
            }

            if (criterio_1_R1 > Evaluation1Consts.Criterio_1_R1MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_1_R1), criterio_1_R1, "The value of 'criterio_1_R1' cannot be greater than " + Evaluation1Consts.Criterio_1_R1MaxLength);
            }

            if (criterio_1_R2 < Evaluation1Consts.Criterio_1_R2MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_1_R2), criterio_1_R2, "The value of 'criterio_1_R2' cannot be lower than " + Evaluation1Consts.Criterio_1_R2MinLength);
            }

            if (criterio_1_R2 > Evaluation1Consts.Criterio_1_R2MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_1_R2), criterio_1_R2, "The value of 'criterio_1_R2' cannot be greater than " + Evaluation1Consts.Criterio_1_R2MaxLength);
            }

            if (criterio_2_R1 < Evaluation1Consts.Criterio_2_R1MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_2_R1), criterio_2_R1, "The value of 'criterio_2_R1' cannot be lower than " + Evaluation1Consts.Criterio_2_R1MinLength);
            }

            if (criterio_2_R1 > Evaluation1Consts.Criterio_2_R1MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_2_R1), criterio_2_R1, "The value of 'criterio_2_R1' cannot be greater than " + Evaluation1Consts.Criterio_2_R1MaxLength);
            }

            if (criterio_2_R2 < Evaluation1Consts.Criterio_2_R2MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_2_R2), criterio_2_R2, "The value of 'criterio_2_R2' cannot be lower than " + Evaluation1Consts.Criterio_2_R2MinLength);
            }

            if (criterio_2_R2 > Evaluation1Consts.Criterio_2_R2MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_2_R2), criterio_2_R2, "The value of 'criterio_2_R2' cannot be greater than " + Evaluation1Consts.Criterio_2_R2MaxLength);
            }

            if (criterio_3_R1 < Evaluation1Consts.Criterio_3_R1MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_3_R1), criterio_3_R1, "The value of 'criterio_3_R1' cannot be lower than " + Evaluation1Consts.Criterio_3_R1MinLength);
            }

            if (criterio_3_R1 > Evaluation1Consts.Criterio_3_R1MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_3_R1), criterio_3_R1, "The value of 'criterio_3_R1' cannot be greater than " + Evaluation1Consts.Criterio_3_R1MaxLength);
            }

            if (criterio_3_R2 < Evaluation1Consts.Criterio_3_R2MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_3_R2), criterio_3_R2, "The value of 'criterio_3_R2' cannot be lower than " + Evaluation1Consts.Criterio_3_R2MinLength);
            }

            if (criterio_3_R2 > Evaluation1Consts.Criterio_3_R2MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_3_R2), criterio_3_R2, "The value of 'criterio_3_R2' cannot be greater than " + Evaluation1Consts.Criterio_3_R2MaxLength);
            }

            if (criterio_4_R1 < Evaluation1Consts.Criterio_4_R1MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_4_R1), criterio_4_R1, "The value of 'criterio_4_R1' cannot be lower than " + Evaluation1Consts.Criterio_4_R1MinLength);
            }

            if (criterio_4_R1 > Evaluation1Consts.Criterio_4_R1MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_4_R1), criterio_4_R1, "The value of 'criterio_4_R1' cannot be greater than " + Evaluation1Consts.Criterio_4_R1MaxLength);
            }

            if (criterio_4_R2 < Evaluation1Consts.Criterio_4_R2MinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_4_R2), criterio_4_R2, "The value of 'criterio_4_R2' cannot be lower than " + Evaluation1Consts.Criterio_4_R2MinLength);
            }

            if (criterio_4_R2 > Evaluation1Consts.Criterio_4_R2MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(criterio_4_R2), criterio_4_R2, "The value of 'criterio_4_R2' cannot be greater than " + Evaluation1Consts.Criterio_4_R2MaxLength);
            }

            Criterio_1_R1 = criterio_1_R1;
            Criterio_1_R2 = criterio_1_R2;
            Criterio_2_R1 = criterio_2_R1;
            Criterio_2_R2 = criterio_2_R2;
            Criterio_3_R1 = criterio_3_R1;
            Criterio_3_R2 = criterio_3_R2;
            Criterio_4_R1 = criterio_4_R1;
            Criterio_4_R2 = criterio_4_R2;
            Resultado_R1 = resultado_R1;
            Resultado_R2 = resultado_R2;
            AthleteId = athleteId;
        }

    }
}