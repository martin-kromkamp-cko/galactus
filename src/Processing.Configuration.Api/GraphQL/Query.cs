using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data;

namespace Processing.Configuration.Api.GraphQL;

public class Query
{
    public IQueryable<Currency> Currencies([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext().Currencies;
    }
}