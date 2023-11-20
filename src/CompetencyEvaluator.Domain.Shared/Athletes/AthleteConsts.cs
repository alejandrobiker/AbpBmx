namespace CompetencyEvaluator.Athletes
{
    public static class AthleteConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Athlete." : string.Empty);
        }

        public const int NameMinLength = 2;
        public const int NameMaxLength = 64;
    }
}