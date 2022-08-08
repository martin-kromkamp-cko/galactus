using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public class CkoServiceResponse
{
    public string Type { get; set; }
    public string? Version { get; set; }
    public string Key { get; set; }

    public static CkoServiceResponse From(CkoService ckoService)
    {
        return new()
        {
            Type = ckoService.Type,
            Version = ckoService.Version,
            Key = ckoService.Key,
        };
    }
}