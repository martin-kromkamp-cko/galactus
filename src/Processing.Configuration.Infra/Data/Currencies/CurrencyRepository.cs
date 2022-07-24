using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Infra.Data.Currencies;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly IDbContextFactory<ProcessingContext> _dbContextFactory;

    public CurrencyRepository(IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IQueryable<Currency> All()
    {
        var ctx = _dbContextFactory.CreateDbContext();

        return ctx.Currencies;
    }

    public async Task<Currency> AddAsync(Currency currency, CancellationToken cancellationToken)
    {
        var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var newCurrency = await ctx.Currencies.AddAsync(currency, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);

        return newCurrency.Entity;
    }

    public async Task<Currency> UpdateAsync(Currency currency, CancellationToken cancellationToken)
    {
        var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        // ctx.Set<Currency>().Attach(currency);
        ctx.Update(currency);
        await ctx.SaveChangesAsync(cancellationToken);

        return currency;
    }
}