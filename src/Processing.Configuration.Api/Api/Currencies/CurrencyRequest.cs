using Processing.Configuration.Currencies;

namespace Processing.Configuration.Api.Api.Currencies;

public class CurrencyRequest
{
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

    public Currency To()
    {
        return Currency.Create(Country, Name, Code, Number);
    }
}