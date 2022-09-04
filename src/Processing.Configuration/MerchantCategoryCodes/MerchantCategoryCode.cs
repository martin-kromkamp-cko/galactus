using Processing.Configuration.Processors;

namespace Processing.Configuration.MerchantCategoryCodes;

public class MerchantCategoryCode : ConfigurationItemBase
{
    public MerchantCategoryCode()
    { }

    internal MerchantCategoryCode(string title, int code) 
        : base(Identifiers.Id.NewId("mcc").ToString())
    {
        Title = title;
        Code = code;
    }

    /// <summary>
    /// Gets the title/name of this <see cref="MerchantCategoryCode"/>.
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Gets the code of this <see cref="MerchantCategoryCode"/>.
    /// </summary>
    public int Code { get; private set; }
    
    /// <summary>
    /// Gets the <see cref="Processor"/> that reference this <see cref="MerchantCategoryCode"/>.
    /// </summary>
    public virtual ICollection<Processor> Processors { get; private set; }

    /// <summary>
    /// Create a new <see cref="MerchantCategoryCode"/>.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="code">The code.</param>
    /// <returns>An <see cref="MerchantCategoryCode"/>.</returns>
    public static MerchantCategoryCode Create(string title, int code)
    {
        return new(title, code);
    }
}