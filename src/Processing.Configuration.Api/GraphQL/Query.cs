using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data;
using Processing.Configuration.Infra.Data.Processing;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.GraphQL;

public class Query
{
    public IQueryable<Currency> Currencies([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext().Currencies;
    }
    
    public IQueryable<CardScheme> CardSchemes([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext().CardSchemes;
    }
}