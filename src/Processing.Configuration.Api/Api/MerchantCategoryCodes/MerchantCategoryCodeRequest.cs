using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Api.Api.MerchantCategoryCodes;

public class MerchantCategoryCodeRequest
{
    /// <summary>
    /// The title of this merchant category code.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// The name of this Merchant category code.
    /// </summary>
    public int Code { get; set; }

    public MerchantCategoryCode To()
    {
        return MerchantCategoryCode.Create(Title, Code);
    }
}