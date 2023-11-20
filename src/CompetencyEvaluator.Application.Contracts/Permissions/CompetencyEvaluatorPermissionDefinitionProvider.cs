using CompetencyEvaluator.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompetencyEvaluator.Permissions;

public class CompetencyEvaluatorPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CompetencyEvaluatorPermissions.GroupName, L("Permission:CompetencyEvaluator"));

        var typeRulePermission = myGroup.AddPermission(CompetencyEvaluatorPermissions.TypeRules.Default, L("Permission:TypeRules"));
        typeRulePermission.AddChild(CompetencyEvaluatorPermissions.TypeRules.Create, L("Permission:Create"));
        typeRulePermission.AddChild(CompetencyEvaluatorPermissions.TypeRules.Edit, L("Permission:Edit"));
        typeRulePermission.AddChild(CompetencyEvaluatorPermissions.TypeRules.Delete, L("Permission:Delete"));

        var genderPermission = myGroup.AddPermission(CompetencyEvaluatorPermissions.Genders.Default, L("Permission:Genders"));
        genderPermission.AddChild(CompetencyEvaluatorPermissions.Genders.Create, L("Permission:Create"));
        genderPermission.AddChild(CompetencyEvaluatorPermissions.Genders.Edit, L("Permission:Edit"));
        genderPermission.AddChild(CompetencyEvaluatorPermissions.Genders.Delete, L("Permission:Delete"));

        var categoryPermission = myGroup.AddPermission(CompetencyEvaluatorPermissions.Categories.Default, L("Permission:Categories"));
        categoryPermission.AddChild(CompetencyEvaluatorPermissions.Categories.Create, L("Permission:Create"));
        categoryPermission.AddChild(CompetencyEvaluatorPermissions.Categories.Edit, L("Permission:Edit"));
        categoryPermission.AddChild(CompetencyEvaluatorPermissions.Categories.Delete, L("Permission:Delete"));

        var athletePermission = myGroup.AddPermission(CompetencyEvaluatorPermissions.Athletes.Default, L("Permission:Athletes"));
        athletePermission.AddChild(CompetencyEvaluatorPermissions.Athletes.Create, L("Permission:Create"));
        athletePermission.AddChild(CompetencyEvaluatorPermissions.Athletes.Edit, L("Permission:Edit"));
        athletePermission.AddChild(CompetencyEvaluatorPermissions.Athletes.Delete, L("Permission:Delete"));

        var evaluation1Permission = myGroup.AddPermission(CompetencyEvaluatorPermissions.Evaluation1s.Default, L("Permission:Evaluation1s"));
        evaluation1Permission.AddChild(CompetencyEvaluatorPermissions.Evaluation1s.Create, L("Permission:Create"));
        evaluation1Permission.AddChild(CompetencyEvaluatorPermissions.Evaluation1s.Edit, L("Permission:Edit"));
        evaluation1Permission.AddChild(CompetencyEvaluatorPermissions.Evaluation1s.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CompetencyEvaluatorResource>(name);
    }
}