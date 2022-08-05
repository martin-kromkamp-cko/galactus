using System.Text.Json.Serialization;
using Processing.Configuration.Api.Api.Processors;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public class ProcessingChannelRequest
{
    public string Name { get; set; }
    
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }
    
    [JsonPropertyName("entity_id")]
    public string EntityId { get; set; }
    
    [JsonPropertyName("merchant_account_id")]
    public long? MerchantAccountId { get; set; }
    public IEnumerable<ProcessorRequest> Processors { get; set; }

    public IEnumerable<CkoServiceRequest> Services { get; set; }

    // TODO: public IEnumerable<ApiKey> ApiKeys { get; set; }
    // TODO: public IEnumerable<ProcessingChannelUrl> Urls { get; set; }
    
    [JsonPropertyName("business_model")]
    public string BusinessModel { get; set; }
    
    public IDictionary<string, string[]?>? Features { get; set; }

    public ProcessingChannel To()
    {
        Enum.TryParse<BusinessModel>(BusinessModel, out var businessModel);

        return ProcessingChannel.Create(Name, ClientId, EntityId, MerchantAccountId, businessModel);
    }
}

public class CkoServiceRequest
{
    public string Type { get; set; }

    public string Key { get; set; }

    public string Version { get; set; }
    
    public CkoService To()
    {
        return CkoService.Create(Type, Key, Version);
    }
}
