using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CompetencyEvaluator.EntityFrameworkCore;

public class CompetencyEvaluatorHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CompetencyEvaluatorHttpApiHostMigrationsDbContext>
{
    public CompetencyEvaluatorHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CompetencyEvaluatorHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("CompetencyEvaluator"));

        return new CompetencyEvaluatorHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
