namespace Processing.Configuration.Schemes;

public interface ICardSchemeService
{
    Task<CardScheme?> GetBySchemeName(string schemeName, CancellationToken cancellationToken);

    Task<ServiceResult<CardScheme>> AddAsync(CardScheme cardScheme, CancellationToken cancellationToken);

    Task<ServiceResult<CardScheme>> DisableAsync(CardScheme cardScheme, CancellationToken cancellationToken);
}