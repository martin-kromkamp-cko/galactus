using Processing.Configuration.Processors;

namespace Processing.Configuration.ProcessingChannels;

public class ProcessingChannel : EntityBase
{
    internal ProcessingChannel()
    { }

    internal ProcessingChannel(string name, string? clientId, string? entityId, long? merchantAccountId, BusinessModel businessModel, ICollection<Processor> processors, ICollection<CkoService> services) 
        : base(Identifiers.Id.NewId("pc").ToString())
    {
        Name = name;
        ClientId = clientId;
        EntityId = entityId;
        MerchantAccountId = merchantAccountId;
        BusinessModel = businessModel;
        Processors = processors;
        Services = services;
    }

    /// <summary>
    /// Gets the name of this processing channel.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the client id of this processing channel.
    /// </summary>
    public string? ClientId { get; private set; }
    
    /// <summary>
    /// Gets the entity id of this processing channel.
    /// </summary>
    public string? EntityId { get; private set; }
    
    /// <summary>
    /// Gets the merchant id of this processing channel.
    /// </summary>
    public long? MerchantAccountId { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="BusinessModel"/> of this processing channel.
    /// </summary>
    public BusinessModel BusinessModel { get; private set; }

    /// <summary>
    /// Gets the <see cref="Processor"/> of this processing channel.
    /// </summary>
    public virtual ICollection<Processor> Processors { get; set; }

    /// <summary>
    /// Gets the <see cref="CkoService"/> of this processing channel.
    /// </summary>
    public virtual ICollection<CkoService> Services { get; set; }

    /// <summary>
    /// Gets the <see cref="ProcessingChannelUrl"/> of this processing channel.
    /// </summary>
    // public virtual ICollection<ProcessingChannelUrl> Urls { get; set; }

    public static ProcessingChannel Create(string name, string? clientId, string? entityId, long? merchantId, BusinessModel businessModel)
    {
        return new(name, clientId, entityId, merchantId, businessModel, new List<Processor>(), new List<CkoService>());
    }
}