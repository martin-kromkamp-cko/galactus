using Processing.Configuration.Processors;

namespace Processing.Configuration.Currencies;

/// <summary>
/// Represents an iso-4217 currency code
/// https://www.iso.org/iso-4217-currency-codes.html
/// </summary>
public class Currency : EntityBase
{
    public Currency()
    { }

    public Currency(string name, string code, int number) 
        : base(Ids.Id.NewId("curr").ToString())
    {
        Name = name;
        Code = code;
        Number = number;
    }

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
    
    /// <summary>
    /// Gets the <see cref="Processor"/> referencing this <see cref="Currency"/>.
    /// </summary>
    public virtual ICollection<Processor> Processors { get; private set; }

    public static Currency Create(string name, string code, int number)
    {
        return new(name, code, number);
    }
}