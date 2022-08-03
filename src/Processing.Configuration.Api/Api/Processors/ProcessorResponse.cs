using Processing.Configuration.Api.Api.ProcessingChannels;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorResponse
{
    public string Id { get; set; }
    public string AcquirerId { get; set; }
    public string Description { get; set; }
    public string MerchantCategoryCode { get; set; }
    public IEnumerable<string> Currencies { get; set; }
    public ProcessorAcceptorResponse Acceptor { get; set; }
    public IEnumerable<string> Schemes { get; set; }
    public bool DynamicDescriptor { get; set; }
    public string DynamicDescriptorPrefix { get; set; }
    // public IDictionary<string, object> Settings { get; set; }
    public IEnumerable<CkoServiceResponse> Services { get; set; }
    public string ProviderKey { get; set; }
    public string Mode { get; set; }
    // public IDictionary<string, string[]?>? Features { get; set; }

    public static ProcessorResponse From(Processor processor)
    {
        return new()
        {
            Id = processor.ExternalId,
            AcquirerId = processor.AcquirerId,
            Description = processor.Description,
            MerchantCategoryCode = processor.MerchantCategoryCode.Code.ToString(),
            Currencies = processor.Currencies.Select(c => c.Code),
            Acceptor = ProcessorAcceptorResponse.From(processor.Acceptor),
            Schemes = processor.Schemes.Select(s => s.Scheme),
            DynamicDescriptor = processor.DynamicDescriptor,
            DynamicDescriptorPrefix = processor.DynamicDescriptorPrefix,
            Services = processor.Services.Select(CkoServiceResponse.From),
            ProviderKey = processor.ProviderKey,
            Mode = processor.Mode.ToString(),
        };
    }
}