using System;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace CompetencyEvaluator.MongoDB;

[DependsOn(
    typeof(CompetencyEvaluatorTestBaseModule),
    typeof(CompetencyEvaluatorMongoDbModule)
    )]
public class CompetencyEvaluatorMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
