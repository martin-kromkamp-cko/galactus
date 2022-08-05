namespace Processing.Configuration.Processors;

public class ProcessorAcceptor : EntityBase
{
    public ProcessorAcceptor()
    { }

    internal ProcessorAcceptor(string schemeMerchantId, string name, string city, string country, string street, string state, string zip, string phone) 
        : base(Identifiers.Id.NewId("pca").ToString())
    {
        SchemeMerchantId = schemeMerchantId;
        Name = name;
        City = city;
        Country = country;
        Street = street;
        State = state;
        Zip = zip;
        Phone = phone;
    }

    public string SchemeMerchantId { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string? Street { get; private set; }
    public string? State { get; private set; }
    public string? Zip { get; private set; }
    public string? Phone { get; private set; }
    
    internal long ProcessorId { get; private set; }
    
    public virtual Processor Processor { get; private set; }

    /// <summary>
    /// Creates a new <see cref="ProcessorAcceptor"/>/
    /// </summary>
    public static ProcessorAcceptor Create(string schemeMerchantId, string name, string city, string country,
        string street, string state, string zip, string phone)
    {
        return new
        (
            schemeMerchantId: schemeMerchantId,
            name: name,
            city: city,
            country: country,
            street: street,
            state: state,
            zip: zip,
            phone: phone
        );
    }
}