using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace CompetencyEvaluator.EntityFrameworkCore;

public class CompetencyEvaluatorHttpApiHostMigrationsDbContext : AbpDbContext<CompetencyEvaluatorHttpApiHostMigrationsDbContext>
{
    public CompetencyEvaluatorHttpApiHostMigrationsDbContext(DbContextOptions<CompetencyEvaluatorHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCompetencyEvaluator();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
    }
}
