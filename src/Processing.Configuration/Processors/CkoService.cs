namespace Processing.Configuration.Processors;

/// <summary>
/// Represents an internal service at Checkout
/// </summary>
public class CkoService : ConfigurationItemBase
{
    internal CkoService()
    { }

    internal CkoService(string type, string key, string version) 
        : base(Identifiers.Id.NewId("pcs").ToString())
    {
        Type = type;
        Key = key;
        Version = version;
    }

    /// <summary>
    /// Gets the service type.
    /// </summary>
    public string Type { get; private set; }
    
    /// <summary>
    /// Gets the service key.
    /// </summary>
    public string Key { get; private set; }
    
    /// <summary>
    /// Gets the service version.
    /// </summary>
    public string Version { get; private set; }

    /// <summary>
    /// Create a new <see cref="CkoService"/>.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="key">The key.</param>
    /// <param name="version">The version.</param>
    /// <returns>An <see cref="CkoService"/></returns>
    public static CkoService Create(string type, string key, string version)
    {
        return new(type, key, version);
    }
}