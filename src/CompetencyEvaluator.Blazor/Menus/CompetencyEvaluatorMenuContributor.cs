using CompetencyEvaluator.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CompetencyEvaluator.Localization;
using Volo.Abp.UI.Navigation;

namespace CompetencyEvaluator.Blazor.Menus;

public class CompetencyEvaluatorMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemTypeRules(context, moduleMenu);

        AddMenuItemGenders(context, moduleMenu);

        AddMenuItemCategories(context, moduleMenu);

        AddMenuItemAthletes(context, moduleMenu);

        AddMenuItemEvaluation1s(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<CompetencyEvaluatorResource>();

        context.Menu.AddItem(new ApplicationMenuItem(
            CompetencyEvaluatorMenus.Prefix,
            displayName: l["Menu:CompetencyEvaluator"],
            "/CompetencyEvaluator",
            icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            CompetencyEvaluatorMenus.Prefix,
            context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:CompetencyEvaluator"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemTypeRules(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.CompetencyEvaluatorMenus.TypeRules,
                context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:TypeRules"],
                "/CompetencyEvaluator/TypeRules",
                icon: "fa fa-file-alt",
                requiredPermissionName: CompetencyEvaluatorPermissions.TypeRules.Default
            )
        );
    }

    private static void AddMenuItemGenders(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.CompetencyEvaluatorMenus.Genders,
                context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:Genders"],
                "/CompetencyEvaluator/Genders",
                icon: "fa fa-file-alt",
                requiredPermissionName: CompetencyEvaluatorPermissions.Genders.Default
            )
        );
    }

    private static void AddMenuItemCategories(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.CompetencyEvaluatorMenus.Categories,
                context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:Categories"],
                "/CompetencyEvaluator/Categories",
                icon: "fa fa-file-alt",
                requiredPermissionName: CompetencyEvaluatorPermissions.Categories.Default
            )
        );
    }

    private static void AddMenuItemAthletes(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.CompetencyEvaluatorMenus.Athletes,
                context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:Athletes"],
                "/CompetencyEvaluator/Athletes",
                icon: "fa fa-file-alt",
                requiredPermissionName: CompetencyEvaluatorPermissions.Athletes.Default
            )
        );
    }

    private static void AddMenuItemEvaluation1s(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.CompetencyEvaluatorMenus.Evaluation1s,
                context.GetLocalizer<CompetencyEvaluatorResource>()["Menu:Evaluation1s"],
                "/CompetencyEvaluator/Evaluation1s",
                icon: "fa fa-file-alt",
                requiredPermissionName: CompetencyEvaluatorPermissions.Evaluation1s.Default
            )
        );
    }
}