using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Schemes;

public class CardSchemeResponse
{
    public string ExternalId { get; set; }

    public bool IsActive { get; set; }

    /// <summary>
    /// Gets the name of this <see cref="CardSchemeResponse"/>.
    /// </summary>
    public string Scheme { get; set; }

    public static CardSchemeResponse From(CardScheme cardScheme)
    {
        return new()
        {
            ExternalId = cardScheme.ExternalId,
            IsActive = cardScheme.IsActive,
            Scheme = cardScheme.Scheme
        };
    }
}