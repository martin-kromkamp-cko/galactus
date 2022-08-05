using System.Text.Json.Serialization;
using Processing.Configuration.Api.Api.ProcessingChannels;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorRequest
{
    public string Id { get; set; }
    
    [JsonPropertyName("acquirer_id")]
    public string AcquirerId { get; set; }
    public string Description { get; set; }
    
    [JsonPropertyName("merchant_category_code")]
    public string MerchantCategoryCode { get; set; }
    public IEnumerable<string> Currencies { get; set; }
    public ProcessorAcceptorRequest Acceptor { get; set; }
    public IEnumerable<string> Schemes { get; set; }

    [JsonPropertyName("dynamic_descriptor")]
    public bool DynamicDescriptor { get; set; }
    
    [JsonPropertyName("dynamic_descriptor_prefix")]
    public string DynamicDescriptorPrefix { get; set; }
    // public IDictionary<string, object> Settings { get; set; }
    public IEnumerable<CkoServiceRequest> Services { get; set; }
    
    [JsonPropertyName("provider_key")]
    public string ProviderKey { get; set; }

    public string Mode { get; set; } = "gateway_only";
    // public IDictionary<string, string[]?>? Features { get; set; }
}