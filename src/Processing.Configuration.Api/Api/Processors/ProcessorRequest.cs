using Processing.Configuration.Api.Api.Currencies;
using Processing.Configuration.Api.Api.ProcessingChannels;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorRequest
{
    public string Id { get; set; }
    public string AcquirerId { get; set; }
    public string Description { get; set; }
    public string MerchantCategoryCode { get; set; }
    public IEnumerable<string> Currencies { get; set; }
    // public ProcessorAcceptor Acceptor { get; set; }
    public IEnumerable<string> Schemes { get; set; }
    public bool DynamicDescriptor { get; set; }
    public string DynamicDescriptorPrefix { get; set; }
    // public IDictionary<string, object> Settings { get; set; }
    public IEnumerable<CkoServiceRequest> Services { get; set; }
    public string ProviderKey { get; set; }
    public string Mode { get; set; }
    // public IDictionary<string, string[]?>? Features { get; set; }

    public async Task<Processor?> ToProcessorAsync(ProcessingChannel processingChannel, ICurrencyService currencyService, ICardSchemeService cardSchemeService, IMerchantCategoryCodeService merchantCategoryCodeService, CancellationToken cancellationToken)
    {
        var mcc = await merchantCategoryCodeService.GetByCodeAsync(Convert.ToInt32(MerchantCategoryCode), cancellationToken);
        if (mcc is null)
            return null;

        List<Currency> currencies = new List<Currency>();
        foreach (var currencyCode in Currencies)
        {
            var c = await currencyService.GetByCodeAsync(currencyCode, cancellationToken);
            if (c is not null)
                currencies.Add(c);
        }
        
        List<CardScheme> schemes = new List<CardScheme>();
        foreach (var scheme in Schemes)
        {
            var s = await cardSchemeService.GetBySchemeName(scheme, cancellationToken);
            if (s is not null)
                schemes.Add(s);
        }
        
        var services = Services.Select(s => s.To()).ToList();

        return Processor.Create(processingChannel, AcquirerId, Description, mcc, currencies, schemes, DynamicDescriptor,
            DynamicDescriptorPrefix, services, ProviderKey, Mode);
    }
}