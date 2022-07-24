using Processing.Configuration.Currencies;

namespace Processing.Configuration.Api.Api.Currencies;

public class CurrencyResponse
{
    public string ExternalId { get; set; }

    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets the country this <see cref="Currency"/> belongs to.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets the name of this <see cref="Currency"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the short code of this <see cref="Currency"/>.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets the number of this <see cref="Currency"/>.
    /// </summary>
    public int Number { get; set; }

    public static CurrencyResponse From(Currency currency)
    {
        return new()
        {
            ExternalId = currency.ExternalId,
            IsActive = currency.IsActive,
            Country = currency.Country,
            Code = currency.Code,
            Name = currency.Name,
            Number = currency.Number,
        };
    }
}