using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.GraphQL.Types;

public class ProcessorAcceptorType : ObjectType<ProcessorAcceptor>
{
    protected override void Configure(IObjectTypeDescriptor<ProcessorAcceptor> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.SchemeMerchantId)
            .Type<StringType>()
            .Name("scheme_merchant_id");
        
        descriptor.Field(x => x.Name)
            .Type<StringType>()
            .Name("name");
        
        descriptor.Field(x => x.City)
            .Type<StringType>()
            .Name("city");
        
        descriptor.Field(x => x.Country)
            .Type<StringType>()
            .Name("country");
        
        descriptor.Field(x => x.Street)
            .Type<StringType>()
            .Name("street");
        
        descriptor.Field(x => x.State)
            .Type<StringType>()
            .Name("state");
        
        descriptor.Field(x => x.Zip)
            .Type<StringType>()
            .Name("zip");
        
        descriptor.Field(x => x.Phone)
            .Type<StringType>()
            .Name("phone");
    }
}