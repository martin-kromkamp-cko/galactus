using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data.Currencies;

namespace Processing.Configuration.Infra;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICurrencyRepository, CurrencyRepository>();

        return services;
    }
}