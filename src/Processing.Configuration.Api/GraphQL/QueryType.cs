using Processing.Configuration.Api.GraphQL.Types;

namespace Processing.Configuration.Api.GraphQL;

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(f => f.Currencies(default!))
            .Type<CurrencyType>()
            .UsePaging(options: new() { DefaultPageSize = 10, MaxPageSize = 50 })
            .UseFiltering()
            .UseSorting();
        
        descriptor
            .Field(f => f.CardSchemes(default!))
            .Type<CardSchemeType>()
            .UsePaging(options: new() { DefaultPageSize = 10, MaxPageSize = 50 })
            .UseFiltering()
            .UseSorting();
        
        descriptor
            .Field(f => f.MerchantCategoryCodes(default!))
            .Type<MerchantCategoryCodeType>()
            .UsePaging(options: new() { DefaultPageSize = 10, MaxPageSize = 50 })
            .UseFiltering()
            .UseSorting();
        
        descriptor
            .Field(f => f.Processors(default!))
            .Type<ProcessorType>()
            .UsePaging(options: new() { DefaultPageSize = 10, MaxPageSize = 50 })
            .UseFiltering()
            .UseSorting();
        
        descriptor
            .Field(f => f.ProcessingChannels(default!))
            .Type<ProcessingChannelType>()
            .UsePaging(options: new() { DefaultPageSize = 10, MaxPageSize = 50 })
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(f => f.ProcessingChannelById(default!, default!, default!))
            .Type<ProcessingChannelType?>();
    }
}