using System.Text.Json.Serialization;
using Processing.Configuration.Api.Api.Processors;
using Processing.Configuration.ProcessingChannels;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public class ProcessingChannelResponse
{
    public string Id { get; set; }
    
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }
    
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }
    
    public string Name { get; set; }
    
    [JsonPropertyName("merchant_account_id")]
    public long? MerchantAccountId { get; set; }
    
    [JsonPropertyName("business_model")]
    public BusinessModel? BusinessModel { get; set; }
    
    public IEnumerable<ProcessorResponse> Processors { get; set; }
    
    public IEnumerable<CkoServiceResponse>? Services { get; set; }
    
    // public IEnumerable<ApiKey> ApiKeys { get; set; }

    public static ProcessingChannelResponse From(ProcessingChannel processingChannel)
    {
        return new()
        {
            Id = processingChannel.ExternalId,
            ClientId = processingChannel.ClientId,
            EntityId = processingChannel.EntityId,
            Name = processingChannel.Name,
            MerchantAccountId = processingChannel.MerchantAccountId,
            BusinessModel = processingChannel.BusinessModel,
            Processors = processingChannel.Processors.Select(ProcessorResponse.From),
            Services = processingChannel.Services?.Select(CkoServiceResponse.From),
        };
    }
}
