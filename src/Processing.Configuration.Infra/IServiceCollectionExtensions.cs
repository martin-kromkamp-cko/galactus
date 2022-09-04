using Audit.Core;
using Audit.PostgreSql.Configuration;
using Audit.WebApi;
using EntityFrameworkCore.ChangeEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Infra.Data.Auditing;
using Processing.Configuration.Infra.Data.Processing;

namespace Processing.Configuration.Infra;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<ProcessingContext>(cfg =>
        {
            cfg.UseLazyLoadingProxies();
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

        services.AddScoped(typeof(IConfigurationItemRepository<>), typeof(ConfigurationItemRepository<>));

        return services;
    }

    public static IServiceCollection AddAuditing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuditContext>(cfg =>
        {
            cfg.UseNpgsql(configuration.GetConnectionString(nameof(AuditContext)),
                    pg =>
                    {
                        pg.EnableRetryOnFailure(1);
                    })
                .UseSnakeCaseNamingConvention();
        });
        
        Audit.Core.Configuration.Setup()
            .UsePostgreSql(config => config
                .ConnectionString(configuration.GetConnectionString(nameof(AuditContext)))
                .TableName("audit_event")
                .IdColumnName("id")
                .DataColumn("data", DataType.JSONB)
                .LastUpdatedColumnName("updated_date")
                .CustomColumn("event_type", ev => ev.EventType));

        return services;
    }
    
    private static readonly string[] AuditMethods = new[] { "POST", "PUT", "PATCH", "DELETE" };

    public static IApplicationBuilder AddApiAuditing(this IApplicationBuilder app)
    {
        app.UseAuditMiddleware(_ => _
            .FilterByRequest(rq => rq.Path.Value.StartsWith("/api") && AuditMethods.Contains(rq.Method))
            .IncludeHeaders()
            .IncludeResponseHeaders()
            .IncludeRequestBody()
            .IncludeResponseBody());

        return app;
    }
}