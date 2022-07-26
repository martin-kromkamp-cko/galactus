using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.Schemes;

namespace Processing.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<Currency>, CurrencyValidator>();
        services.AddScoped<IValidator<CardScheme>, CardSchemeValidator>();
        services.AddScoped<IValidator<MerchantCategoryCode>, MerchantCategoryCodeValidator>();
        
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ICardSchemeService, CardSchemeService>();
        services.AddScoped<IMerchantCategoryCodeService, MerchantCategoryCodeService>();

        return services;
    }
}