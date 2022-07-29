using System.Diagnostics;
using Processing.Configuration.Api.Api.Processors;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public class ProcessingChannelResponse
{
    public string Id { get; set; }
    public IEnumerable<ProcessorResponse> Processors { get; set; }
    // public IEnumerable<ApiKey> ApiKeys { get; set; }

    public static ProcessingChannelResponse From(ProcessingChannel processingChannel)
    {
        return new()
        {
            Id = processingChannel.ExternalId,
            Processors = processingChannel.Processors.Select(ProcessorResponse.From)
        };
    }
}

public class CkoServiceResponse
{

    public static CkoServiceResponse From(CkoService ckoService)
    {
        return new();
    }
}