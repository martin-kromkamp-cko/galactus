namespace Processing.Configuration.Currencies;

public interface ICurrencyService
{
    /// <summary>
    /// Gets a <see cref="Currency"/> by code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Currency"/> if any, otherwise null.</returns>
    Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a <see cref="Currency"/> by externalId.
    /// </summary>
    /// <param name="externalId">The external id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Currency"/> if any, otherwise null.</returns>
    Task<Currency?> GetByExternalId(string externalId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Adds a <see cref="Currency"/>.
    /// </summary>
    /// <param name="currency">The currency.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Currency"/>.</returns>
    Task<ServiceResult<Currency>> AddAsync(Currency currency, CancellationToken cancellationToken);

    /// <summary>
    /// Disables a <see cref="Currency"/>.
    /// </summary>
    /// <param name="currency">The currency.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The disabled <see cref="Currency"/>.</returns>
    Task<ServiceResult<Currency>> DisableAsync(Currency currency, CancellationToken cancellationToken);
}