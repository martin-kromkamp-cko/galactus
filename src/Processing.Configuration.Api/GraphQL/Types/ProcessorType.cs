using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.GraphQL.Types;

public class ProcessorType : ObjectType<Processor>
{
    protected override void Configure(IObjectTypeDescriptor<Processor> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.ExternalId)
            .ID()
            .Name("id");
        
        descriptor.Field(x => x.IsActive)
            .Type<BooleanType>()
            .Name("is_active");
        
        descriptor.Field(x => x.Description)
            .Type<StringType>()
            .Name("description");

        descriptor.Field(x => x.AcquirerId)
            .Type<StringType>()
            .Name("acquirer_id");
        
        descriptor.Field(x => x.MerchantCategoryCode)
            .Type<MerchantCategoryCodeType>()
            .Name("merchant_category_code");
        
        descriptor.Field(x => x.Currencies)
            .Type<ListType<CurrencyType>>()
            .Name("currencies");
        
        descriptor.Field(x => x.Schemes)
            .Type<ListType<CardSchemeType>>()
            .Name("schemes");
        
        descriptor.Field(x => x.DynamicDescriptor)
            .Type<BooleanType>()
            .Name("dynamic_descriptor");
        
        descriptor.Field(x => x.DynamicDescriptorPrefix)
            .Type<StringType>()
            .Name("dynamic_descriptor_prefix");
        
        descriptor.Field(x => x.Services)
            .Type<ListType<CkoServiceType>>()
            .Name("services");
        
        descriptor.Field(x => x.ProviderKey)
            .Type<StringType>()
            .Name("providerKey");
        
        descriptor.Field(x => x.Mode)
            .Type<StringType>()
            .Name("mode");

        descriptor.Field(x => x.ProcessingChannel)
            .Type<ProcessingChannelType>()
            .Name("processing_channel");
    }
}