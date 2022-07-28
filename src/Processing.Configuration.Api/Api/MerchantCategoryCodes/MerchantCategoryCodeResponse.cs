using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Api.Api.MerchantCategoryCodes;

public class MerchantCategoryCodeResponse
{
    public string ExternalId { get; set; }

    public bool IsActive { get; set; }

    /// <summary>
    /// Gets the title of this <see cref="MerchantCategoryCodeResponse"/>.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets the code of this <see cref="MerchantCategoryCodeResponse"/>.
    /// </summary>
    public int Code { get; set; }

    public static MerchantCategoryCodeResponse From(MerchantCategoryCode mcc)
    {
        return new()
        {
            ExternalId = mcc.ExternalId,
            IsActive = mcc.IsActive,
            Title = mcc.Title,
            Code = mcc.Code,
        };
    }
}