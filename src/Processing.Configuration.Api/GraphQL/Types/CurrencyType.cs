using Processing.Configuration.Currencies;

namespace Processing.Configuration.Api.GraphQL.Types;

public class CurrencyType : ObjectType<Currency>
{
    protected override void Configure(IObjectTypeDescriptor<Currency> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Country)
            .Type<StringType>()
            .Name("country");
        
        descriptor.Field(x => x.Name)
            .Type<StringType>()
            .Name("name");
        
        descriptor.Field(x => x.Code)
            .Type<StringType>()
            .Name("code");
        
        descriptor.Field(x => x.Number)
            .Type<IntType>()
            .Name("number");
        
        descriptor.Field(x => x.Processors)
            .Type<ListType<ProcessorType>>()
            .Name("processors");
    }
}