namespace Processing.Configuration.Currencies;

public interface ICurrencyRepository
{
    IQueryable<Currency> All();

    Task<Currency> AddAsync(Currency currency, CancellationToken cancellationToken);

    Task<Currency> UpdateAsync(Currency currency, CancellationToken cancellationToken);
}