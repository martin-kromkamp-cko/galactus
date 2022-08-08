using Microsoft.EntityFrameworkCore;
using Processing.Configuration.Currencies;
using Processing.Configuration.Infra.Data.Processing;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.GraphQL;

public class Query
{
    public IQueryable<Currency> Currencies([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext()
            .Currencies
            .Include(x => x.Processors)
                .ThenInclude(x => x.ProcessingChannel);
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
            .Include(x => x.MerchantCategoryCode)
            .Include(x => x.Acceptor)
            .Include(x => x.Currencies)
            .AsSplitQuery()
            .Include(x => x.Schemes)
            .AsSplitQuery()
            .Include(x => x.Services)
            .AsSplitQuery()
            .Include(x => x.ProcessingChannel)
            .AsSplitQuery();
    }
    
    public IQueryable<ProcessingChannel> ProcessingChannels([Service] IDbContextFactory<ProcessingContext> dbContextFactory)
    {
        return dbContextFactory.CreateDbContext()
            .ProcessingChannels
            .Include(x => x.Processors)
            .AsSplitQuery()
            .Include(x => x.Services)
            .AsSplitQuery();
    }
    
    public ProcessingChannel? ProcessingChannelById([Service] IDbContextFactory<ProcessingContext> dbContextFactory, string processingChannelId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(processingChannelId))
            return null;

        return dbContextFactory.CreateDbContext()
            .ProcessingChannels
            .Include(x => x.Processors)
            .ThenInclude(x => x.Currencies)
            .FirstOrDefault(x => x.ExternalId == processingChannelId);
    }
}