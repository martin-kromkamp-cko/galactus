using EntityFrameworkCore.ChangeEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data;
using Processing.Configuration.Infra.Data.Currencies;

namespace Processing.Configuration.Infra;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<ProcessingContext>(cfg =>
        {
            cfg.UseNpgsql(configuration.GetConnectionString(nameof(ProcessingContext)),
                    pg =>
                    {
                        pg.EnableRetryOnFailure(2);
                    })
                .UseSnakeCaseNamingConvention();
            cfg.EnableDetailedErrors();
            cfg.UseChangeEvents();
        });

        services.AddScoped(svc => svc.GetRequiredService<IDbContextFactory<ProcessingContext>>().CreateDbContext());
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        return services;
    }
}