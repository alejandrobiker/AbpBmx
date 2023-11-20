using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using CompetencyEvaluator.Localization;
using CompetencyEvaluator.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using CompetencyEvaluator.Permissions;

namespace CompetencyEvaluator.Web;

[DependsOn(
    typeof(CompetencyEvaluatorApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class CompetencyEvaluatorWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(CompetencyEvaluatorResource), typeof(CompetencyEvaluatorWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CompetencyEvaluatorWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CompetencyEvaluatorMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CompetencyEvaluatorWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<CompetencyEvaluatorWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CompetencyEvaluatorWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/TypeRules/Index", CompetencyEvaluatorPermissions.TypeRules.Default);
            options.Conventions.AuthorizePage("/Genders/Index", CompetencyEvaluatorPermissions.Genders.Default);
            options.Conventions.AuthorizePage("/Categories/Index", CompetencyEvaluatorPermissions.Categories.Default);
            options.Conventions.AuthorizePage("/Athletes/Index", CompetencyEvaluatorPermissions.Athletes.Default);
            options.Conventions.AuthorizePage("/Evaluation1s/Index", CompetencyEvaluatorPermissions.Evaluation1s.Default);
        });
    }
}