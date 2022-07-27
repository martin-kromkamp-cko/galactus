using Processing.Configuration.Processors;

namespace Processing.Configuration.Schemes;

public class CardScheme : EntityBase
{
    public CardScheme()
    { }

    internal CardScheme(string scheme) 
        : base(Ids.Id.NewId("cs").ToString())
    {
        Scheme = scheme;
    }

    /// <summary>
    /// Gets the scheme name of the <see cref="CardScheme"/>.
    /// </summary>
    public string Scheme { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="Processor"/> that reference this <see cref="CardScheme"/>
    /// </summary>
    public virtual ICollection<Processor> Processors { get; private set; }

    /// <summary>
    /// Create a new <see cref="CardScheme"/>
    /// </summary>
    /// <param name="schemeName">The name of the scheme</param>
    /// <returns>An <see cref="CardScheme"/>.</returns>
    public static CardScheme Create(string schemeName)
    {
        return new CardScheme(schemeName);
    }
}