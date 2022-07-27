using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Schemes;

public class CardSchemeRequest
{
    /// <summary>
    /// Gets the name of this card scheme.
    /// </summary>
    public string Scheme { get; set; }

    public CardScheme To()
    {
        return CardScheme.Create(Scheme);
    }
}