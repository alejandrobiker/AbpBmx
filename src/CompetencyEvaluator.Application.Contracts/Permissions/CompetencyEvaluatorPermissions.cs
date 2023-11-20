using Volo.Abp.Reflection;

namespace CompetencyEvaluator.Permissions;

public class CompetencyEvaluatorPermissions
{
    public const string GroupName = "CompetencyEvaluator";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CompetencyEvaluatorPermissions));
    }

    public static class TypeRules
    {
        public const string Default = GroupName + ".TypeRules";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Genders
    {
        public const string Default = GroupName + ".Genders";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Categories
    {
        public const string Default = GroupName + ".Categories";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Athletes
    {
        public const string Default = GroupName + ".Athletes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Evaluation1s
    {
        public const string Default = GroupName + ".Evaluation1s";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}