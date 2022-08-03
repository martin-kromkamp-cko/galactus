using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.Processors;

public class ProcessorAcceptorResponse
{
    public string Id { get; set; }
    public string SchemeMerchantId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Phone { get; set; }

    public static ProcessorAcceptorResponse From(ProcessorAcceptor acceptor)
    {
        return new()
        {
            Id = acceptor.ExternalId,
            SchemeMerchantId = acceptor.SchemeMerchantId,
            Name = acceptor.Name,
            City = acceptor.City,
            Country = acceptor.Country,
            Street = acceptor.Street,
            State = acceptor.State,
            Zip = acceptor.Zip,
            Phone = acceptor.Phone,
        };
    }
}