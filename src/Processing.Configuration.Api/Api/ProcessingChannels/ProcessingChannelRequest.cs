using Processing.Configuration.Api.Api.Processors;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public class ProcessingChannelRequest
{
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string EntityId { get; set; }
    public long? MerchantAccountId { get; set; }
    public IEnumerable<ProcessorRequest> Processors { get; set; }

    public IEnumerable<CkoServiceRequest> Services { get; set; }

    // public IEnumerable<ApiKey> ApiKeys { get; set; }
    // public IEnumerable<ProcessingChannelUrl> Urls { get; set; }
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
