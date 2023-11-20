namespace CompetencyEvaluator.Genders
{
    public static class GenderConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Gender." : string.Empty);
        }

        public const int nameMinLength = 3;
        public const int nameMaxLength = 10;
        public const int ShortNameMaxLength = 1;
    }
}