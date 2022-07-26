namespace Processing.Configuration.MerchantCategoryCodes;

public interface IMerchantCategoryCodeService
{
    /// <summary>
    /// Gets a <see cref="MerchantCategoryCode"/> by code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="MerchantCategoryCode"/> if any, otherwise null.</returns>
    Task<MerchantCategoryCode?> GetByCodeAsync(int code, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a <see cref="MerchantCategoryCode"/>.
    /// </summary>
    /// <param name="merchantCategoryCode">The merchant category code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="MerchantCategoryCode"/>.</returns>
    Task<ServiceResult<MerchantCategoryCode>> AddAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken);

    /// <summary>
    /// Disables a <see cref="MerchantCategoryCode"/>.
    /// </summary>
    /// <param name="merchantCategoryCode">The merchant category code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The disabled <see cref="MerchantCategoryCode"/>.</returns>
    Task<ServiceResult<MerchantCategoryCode>> DisableAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken);
}