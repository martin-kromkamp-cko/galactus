using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<Currency>, CurrencyValidator>();
        services.AddScoped<IValidator<CardScheme>, CardSchemeValidator>();
        services.AddScoped<IValidator<MerchantCategoryCode>, MerchantCategoryCodeValidator>();
        services.AddScoped<IValidator<CkoService>, ProcessorServiceValidator>();
        services.AddScoped<IValidator<Processor>, ProcessorValidator>();
        services.AddScoped<IValidator<ProcessingChannel>, ProcessingChannelValidator>();
        
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ICardSchemeService, CardSchemeService>();
        services.AddScoped<IMerchantCategoryCodeService, MerchantCategoryCodeService>();
        services.AddScoped<IProcessorService, ProcessorService>();
        services.AddScoped<IProcessingChannelService, ProcessingChannelService>();

        return services;
    }
}