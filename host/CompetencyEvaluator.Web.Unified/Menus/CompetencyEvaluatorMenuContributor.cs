﻿using System.Threading.Tasks;
using CompetencyEvaluator.Web.Menus;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace CompetencyEvaluator.Menus;

public class CompetencyEvaluatorMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.User)
        {
            return Task.CompletedTask;
        }

        context.Menu.SetSubItemOrder(CompetencyEvaluatorMenus.Prefix, 1);

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 2);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 3;

        //Administration -> Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration -> Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 2);

        //Administration -> Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
