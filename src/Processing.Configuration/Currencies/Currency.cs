namespace Processing.Configuration.Currencies;

/// <summary>
/// Represents an iso-4217 currency code
/// https://www.iso.org/iso-4217-currency-codes.html
/// </summary>
public class Currency : EntityBase
{
    internal Currency()
    {
    }

    public Currency(string externalId, string country, string name, string code, int number) 
        : base(externalId)
    {
        Country = country;
        Name = name;
        Code = code;
        Number = number;
    }

    /// <summary>
    /// Gets the country this <see cref="Currency"/> belongs to.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// Gets the name of this <see cref="Currency"/>.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the short code of this <see cref="Currency"/>.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets the number of this <see cref="Currency"/>.
    /// </summary>
    public int Number { get; private set; }

    public static Currency Create(string country, string name, string code, int number)
    {
        return new(Ids.Id.NewId("curr").Value, country, name, code, number);
    }
}