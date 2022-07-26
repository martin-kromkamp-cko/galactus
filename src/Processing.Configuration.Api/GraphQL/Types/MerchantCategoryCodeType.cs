using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Api.GraphQL.Types;

public class MerchantCategoryCodeType : ObjectType<MerchantCategoryCode>
{
    protected override void Configure(IObjectTypeDescriptor<MerchantCategoryCode> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Title)
            .Type<StringType>()
            .Name("title");
        
        descriptor.Field(x => x.Code)
            .Type<StringType>()
            .Name("code");
    }
}