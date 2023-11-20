using Volo.Abp.Application.Dtos;
using System;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1ExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public double? Criterio_1_R1Min { get; set; }
        public double? Criterio_1_R1Max { get; set; }
        public double? Criterio_1_R2Min { get; set; }
        public double? Criterio_1_R2Max { get; set; }
        public double? Criterio_2_R1Min { get; set; }
        public double? Criterio_2_R1Max { get; set; }
        public double? Criterio_2_R2Min { get; set; }
        public double? Criterio_2_R2Max { get; set; }
        public double? Criterio_3_R1Min { get; set; }
        public double? Criterio_3_R1Max { get; set; }
        public double? Criterio_3_R2Min { get; set; }
        public double? Criterio_3_R2Max { get; set; }
        public double? Criterio_4_R1Min { get; set; }
        public double? Criterio_4_R1Max { get; set; }
        public double? Criterio_4_R2Min { get; set; }
        public double? Criterio_4_R2Max { get; set; }
        public double? Resultado_R1Min { get; set; }
        public double? Resultado_R1Max { get; set; }
        public double? Resultado_R2Min { get; set; }
        public double? Resultado_R2Max { get; set; }
        public Guid? AthleteId { get; set; }

        public Evaluation1ExcelDownloadDtoBase()
        {

        }
    }
}