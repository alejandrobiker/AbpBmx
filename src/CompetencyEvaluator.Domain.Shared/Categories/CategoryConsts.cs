namespace CompetencyEvaluator.Categories
{
    public static class CategoryConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Category." : string.Empty);
        }

        public const int NameMinLength = 3;
        public const int NameMaxLength = 64;
        public const int MaxAgeMinLength = 0;
        public const int MaxAgeMaxLength = 99;
    }
}