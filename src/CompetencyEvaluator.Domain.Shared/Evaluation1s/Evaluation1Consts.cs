namespace CompetencyEvaluator.Evaluation1s
{
    public static class Evaluation1Consts
    {
        private const string DefaultSorting = "{0}Criterio_1_R1 asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Evaluation1." : string.Empty);
        }

        public const double Criterio_1_R1MinLength = 0;
        public const double Criterio_1_R1MaxLength = 100;
        public const double Criterio_1_R2MinLength = 0;
        public const double Criterio_1_R2MaxLength = 100;
        public const double Criterio_2_R1MinLength = 0;
        public const double Criterio_2_R1MaxLength = 100;
        public const double Criterio_2_R2MinLength = 0;
        public const double Criterio_2_R2MaxLength = 100;
        public const double Criterio_3_R1MinLength = 0;
        public const double Criterio_3_R1MaxLength = 100;
        public const double Criterio_3_R2MinLength = 0;
        public const double Criterio_3_R2MaxLength = 100;
        public const double Criterio_4_R1MinLength = 0;
        public const double Criterio_4_R1MaxLength = 100;
        public const double Criterio_4_R2MinLength = 0;
        public const double Criterio_4_R2MaxLength = 100;
    }
}