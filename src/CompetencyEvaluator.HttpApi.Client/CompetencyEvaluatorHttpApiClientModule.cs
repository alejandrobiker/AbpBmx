using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace CompetencyEvaluator;

[DependsOn(
    typeof(CompetencyEvaluatorApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CompetencyEvaluatorHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CompetencyEvaluatorApplicationContractsModule).Assembly,
            CompetencyEvaluatorRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CompetencyEvaluatorHttpApiClientModule>();
        });
    }
}
