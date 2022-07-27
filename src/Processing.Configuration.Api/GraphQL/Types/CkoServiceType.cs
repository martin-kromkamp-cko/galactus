using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.GraphQL.Types;

public class CkoServiceType : ObjectType<CkoService>
{
    protected override void Configure(IObjectTypeDescriptor<CkoService> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Type)
            .Type<StringType>()
            .Name("dynamic_descriptor_prefix");
        
        descriptor.Field(x => x.Key)
            .Type<StringType>()
            .Name("services");
        
        descriptor.Field(x => x.Version)
            .Type<StringType>()
            .Name("schemes");
    }
}