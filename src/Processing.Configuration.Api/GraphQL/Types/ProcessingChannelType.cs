using Processing.Configuration.ProcessingChannels;

namespace Processing.Configuration.Api.GraphQL.Types;

public class ProcessingChannelType : ObjectType<ProcessingChannel>
{
    protected override void Configure(IObjectTypeDescriptor<ProcessingChannel> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Name)
            .Type<StringType>()
            .Name("name");
        
        descriptor.Field(x => x.ClientId)
            .Type<StringType>()
            .Name("client_id");
        
        descriptor.Field(x => x.EntityId)
            .Type<StringType>()
            .Name("entity_id");
        
        descriptor.Field(x => x.MerchantAccountId)
            .Type<LongType>()
            .Name("merchant_account_id");
        
        descriptor.Field(x => x.BusinessModel)
            .Type<EnumType<BusinessModel>>()
            .Name("business_model");
        
        descriptor.Field(x => x.Processors)
            .Type<ListType<ProcessorType>>()
            .Name("processors");
        
        descriptor.Field(x => x.Services)
            .Type<ListType<CkoServiceType>>()
            .Name("services");
    }
}