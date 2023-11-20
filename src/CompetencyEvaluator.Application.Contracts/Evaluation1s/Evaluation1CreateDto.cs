using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CompetencyEvaluator.Evaluation1s
{
    public abstract class Evaluation1CreateDtoBase
    {
        [Range(Evaluation1Consts.Criterio_1_R1MinLength, Evaluation1Consts.Criterio_1_R1MaxLength)]
        public double Criterio_1_R1 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_1_R2MinLength, Evaluation1Consts.Criterio_1_R2MaxLength)]
        public double Criterio_1_R2 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_2_R1MinLength, Evaluation1Consts.Criterio_2_R1MaxLength)]
        public double Criterio_2_R1 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_2_R2MinLength, Evaluation1Consts.Criterio_2_R2MaxLength)]
        public double Criterio_2_R2 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_3_R1MinLength, Evaluation1Consts.Criterio_3_R1MaxLength)]
        public double Criterio_3_R1 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_3_R2MinLength, Evaluation1Consts.Criterio_3_R2MaxLength)]
        public double Criterio_3_R2 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_4_R1MinLength, Evaluation1Consts.Criterio_4_R1MaxLength)]
        public double Criterio_4_R1 { get; set; } = 0;
        [Range(Evaluation1Consts.Criterio_4_R2MinLength, Evaluation1Consts.Criterio_4_R2MaxLength)]
        public double Criterio_4_R2 { get; set; } = 0;
        public double Resultado_R1 { get; set; } = 0;
        public double Resultado_R2 { get; set; } = 0;
        public Guid AthleteId { get; set; }
    }
}