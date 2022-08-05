using Processing.Configuration.Currencies;
using Processing.Configuration.Infra;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Processors;

public interface IProcessorMapper
{
    Task<(Processor? Processor, IEnumerable<string> Errors)> MapToAsync(ProcessorRequest request, ProcessingChannel processingChannel, CancellationToken cancellationToken);
}

public class ProcessorMapper : IProcessorMapper
{
    private readonly ICurrencyService _currencyService;
    private readonly ICardSchemeService _cardSchemeService;
    private readonly IMerchantCategoryCodeService _merchantCategoryCodeService;

    public ProcessorMapper(ICurrencyService currencyService, ICardSchemeService cardSchemeService, IMerchantCategoryCodeService merchantCategoryCodeService)
    {
        _currencyService = currencyService;
        _cardSchemeService = cardSchemeService;
        _merchantCategoryCodeService = merchantCategoryCodeService;
    }

    public async Task<(Processor?, IEnumerable<string>)> MapToAsync(ProcessorRequest request, ProcessingChannel processingChannel, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        
        if (!EnumUtils.TryParseWithMemberName<ProcessingMode>(request.Mode, out var processingMode))
            errors.Add($"invalid_mode_{request.Mode}");
        
        var mcc = await _merchantCategoryCodeService.GetByCodeAsync(Convert.ToInt32(request.MerchantCategoryCode), cancellationToken);
        if (mcc is null)
            errors.Add("mcc_not_found");

        var currencies = await _currencyService.GetByCodesAsync(request.Currencies, cancellationToken);
        errors.AddRange(currencies.Where(c => c.Value is null).Select(c => $"currency_{c}_not)_found"));

        List<CardScheme> schemes = new List<CardScheme>();
        foreach (var scheme in request.Schemes)
        {
            var s = await _cardSchemeService.GetBySchemeName(scheme, cancellationToken);
            
            if (s is not null)
                schemes.Add(s);
            else
                errors.Add($"scheme_{scheme}_not_found");
        }
        
        var services = request.Services?.Select(s => s.To()).ToList();

        return errors.Any()
            ? (default, errors)
            : (
                Processor.Create(
                    processingChannel,
                    request.AcquirerId,
                    request.Description,
                    mcc,
                    currencies.Where(c => c.Value is not null).Select(c => c.Value!).ToList(),
                    request.Acceptor.To(),
                    schemes,
                    request.DynamicDescriptor,
                    request.DynamicDescriptorPrefix,
                    services,
                    request.ProviderKey,
                    processingMode)
                , errors);
    }
}