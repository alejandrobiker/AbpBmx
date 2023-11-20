using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1DtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public double Criterio_1_R1 { get; set; }
        public double Criterio_1_R2 { get; set; }
        public double Criterio_2_R1 { get; set; }
        public double Criterio_2_R2 { get; set; }
        public double Criterio_3_R1 { get; set; }
        public double Criterio_3_R2 { get; set; }
        public double Criterio_4_R1 { get; set; }
        public double Criterio_4_R2 { get; set; }
        public double Resultado_R1 { get; set; }
        public double Resultado_R2 { get; set; }
        public Guid AthleteId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}