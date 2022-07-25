using Processing.Configuration.Currencies;

namespace Processing.Configuration.Infra.Data.Currencies;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ProcessingContext _dbContext;

    public CurrencyRepository(ProcessingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Currency> All()
    {
        return _dbContext.Currencies;
    }

    public async Task<Currency> AddAsync(Currency currency, CancellationToken cancellationToken)
    {
        var newCurrency = await _dbContext.Currencies.AddAsync(currency, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newCurrency.Entity;
    }

    public async Task<Currency> UpdateAsync(Currency currency, CancellationToken cancellationToken)
    {
        _dbContext.Update(currency);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return currency;
    }
}