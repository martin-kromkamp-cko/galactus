using System.Text.Json.Serialization;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorAcceptorRequest
{
    [JsonPropertyName("scheme_merchant_id")]
    public string SchemeMerchantId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Phone { get; set; }

    public ProcessorAcceptor To()
    {
        return ProcessorAcceptor.Create(
            SchemeMerchantId,
            Name,
            City,
            Country,
            Street,
            State,
            Zip,
            Phone
        );
    }
}