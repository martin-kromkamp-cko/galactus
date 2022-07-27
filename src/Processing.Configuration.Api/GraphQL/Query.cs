using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data;
using Processing.Configuration.Infra.Data.Processing;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.GraphQL;

public class Query
{
    public IQueryable<Currency> Currencies([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext()
            .Currencies
            .Include(x => x.Processors);
    }
    
    public IQueryable<CardScheme> CardSchemes([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext().CardSchemes;
    }
    
    public IQueryable<MerchantCategoryCode> MerchantCategoryCodes([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext().MerchantCategoryCodes;
    }
    
    public IQueryable<Processor> Processors([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext()
            .Processors
            .Include(x => x.Currencies)
            .Include(x => x.Schemes)
            .Include(x => x.MerchantCategoryCode)
            .Include(x => x.Services);
    }
}