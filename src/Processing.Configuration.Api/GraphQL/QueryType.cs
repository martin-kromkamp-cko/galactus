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
    }
}