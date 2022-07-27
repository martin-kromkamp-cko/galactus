using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Processors;

public class Processor : EntityBase
{
    internal Processor()
    { }

    internal Processor(string acquirerId, string description, MerchantCategoryCode merchantCategoryCode, ICollection<Currency> currencies, ICollection<CardScheme> schemes, bool dynamicDescriptor, string dynamicDescriptorPrefix, ICollection<CkoService> services, string providerKey, string mode) 
        : base(Ids.Id.NewId("pr").ToString())
    {
        AcquirerId = acquirerId;
        Description = description;
        MerchantCategoryCode = merchantCategoryCode;
        Currencies = currencies;
        Schemes = schemes;
        DynamicDescriptor = dynamicDescriptor;
        DynamicDescriptorPrefix = dynamicDescriptorPrefix;
        Services = services;
        ProviderKey = providerKey;
        Mode = mode;
    }

    /// <summary>
    /// Gets the Acquirer id.
    /// </summary>
    public string AcquirerId { get; private set; }
    
    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="MerchantCategoryCode"/>.
    /// </summary>
    public virtual MerchantCategoryCode MerchantCategoryCode { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="Currency"/>.
    /// </summary>
    public virtual ICollection<Currency>? Currencies { get; private set; }
    
    /// <summary>
    /// Gets the acceptor.
    /// </summary>
    // public ProcessorAcceptor Acceptor { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="CardScheme"/>.
    /// </summary>
    public virtual ICollection<CardScheme>? Schemes { get; private set; }
    
    /// <summary>
    /// Gets the dynamic descriptor.
    /// </summary>
    public bool DynamicDescriptor { get; private set; }
    
    /// <summary>
    /// Gets the dynamic descriptor prefix.
    /// </summary>
    public string DynamicDescriptorPrefix { get; private set; }
    
    // public IDictionary<string, object> Settings { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="CkoService"/>.
    /// </summary>
    public virtual ICollection<CkoService>? Services { get; private set; }

    /// <summary>
    /// Gets or the provider key used by card processing to load the configuration.
    /// </summary>
    public string ProviderKey { get; private set; }
    
    /// <summary>
    /// Gets the mode.
    /// </summary>
    public string Mode { get; private set; }

    /// <summary>
    /// Gets the features.
    /// </summary>
    // public IDictionary<string, string[]?>? Features { get; private set; }

    /// <summary>
    /// Gets the <see cref="ProcessingChannel"/>.
    /// </summary>
    public virtual ProcessingChannel ProcessingChannel { get; private set; }
    
    public static Processor Create(string acquirerId, string description, MerchantCategoryCode merchantCategoryCode, ICollection<Currency> currencies, ICollection<CardScheme> schemes, bool dynamicDescriptor, string dynamicDescriptorPrefix, ICollection<CkoService> services, string providerKey, string mode)
    {
        return new Processor(
            acquirerId: acquirerId, 
            description: description, 
            merchantCategoryCode: merchantCategoryCode,
            currencies: currencies,
            schemes: schemes,
            dynamicDescriptor: dynamicDescriptor,
            dynamicDescriptorPrefix: dynamicDescriptorPrefix,
            services: services, 
            providerKey: providerKey,
            mode: mode);
    }
}