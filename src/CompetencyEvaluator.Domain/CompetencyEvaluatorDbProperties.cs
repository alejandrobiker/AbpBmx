namespace CompetencyEvaluator;

public static class CompetencyEvaluatorDbProperties
{
    public static string DbTablePrefix { get; set; } = "CompetencyEvaluator";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "CompetencyEvaluator";
}
