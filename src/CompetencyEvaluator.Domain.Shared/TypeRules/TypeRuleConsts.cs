namespace CompetencyEvaluator.TypeRules
{
    public static class TypeRuleConsts
    {
        private const string DefaultSorting = "{0}name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TypeRule." : string.Empty);
        }

        public const int nameMinLength = 3;
        public const int nameMaxLength = 32;
    }
}