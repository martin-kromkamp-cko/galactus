namespace Processing.Configuration.Schemes;

public class CardScheme : EntityBase
{
    internal CardScheme()
    { }

    internal CardScheme(string scheme) 
        : base(Ids.Id.NewId("cs").ToString())
    {
        Scheme = scheme;
    }

    /// <summary>
    /// Gets the <see cref="CardScheme"/>.
    /// </summary>
    public string Scheme { get; set; }

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