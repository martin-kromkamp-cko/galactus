using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Currencies;

namespace Processing.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidator<Currency>, CurrencyValidator>();
        services.AddSingleton<ICurrencyService, CurrencyService>();

        return services;
    }
}