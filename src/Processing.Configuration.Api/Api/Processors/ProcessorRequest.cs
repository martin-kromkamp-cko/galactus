using Processing.Configuration.Api.Api.ProcessingChannels;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorRequest
{
    public string Id { get; set; }
    public string AcquirerId { get; set; }
    public string Description { get; set; }
    public string MerchantCategoryCode { get; set; }
    public IEnumerable<string> Currencies { get; set; }
    public ProcessorAcceptorRequest Acceptor { get; set; }
    public IEnumerable<string> Schemes { get; set; }
    public bool DynamicDescriptor { get; set; }
    public string DynamicDescriptorPrefix { get; set; }
    // public IDictionary<string, object> Settings { get; set; }
    public IEnumerable<CkoServiceRequest> Services { get; set; }
    public string ProviderKey { get; set; }
    public string Mode { get; set; }
    // public IDictionary<string, string[]?>? Features { get; set; }
}