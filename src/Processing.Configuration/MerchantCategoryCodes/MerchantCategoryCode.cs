namespace Processing.Configuration.MerchantCategoryCodes;

public class MerchantCategoryCode : EntityBase
{
    internal MerchantCategoryCode()
    { }

    internal MerchantCategoryCode(string title, int code) 
        : base(Ids.Id.NewId("mcc").ToString())
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