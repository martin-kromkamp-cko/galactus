using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.GraphQL.Types;

public class CardSchemeType : ObjectType<CardScheme>
{
    protected override void Configure(IObjectTypeDescriptor<CardScheme> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Scheme)
            .Type<StringType>()
            .Name("scheme");
    }
}