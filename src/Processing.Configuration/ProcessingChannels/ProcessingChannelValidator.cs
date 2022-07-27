using FluentValidation;

namespace Processing.Configuration.ProcessingChannels;

public class ProcessingChannelValidator : AbstractValidator<ProcessingChannel>
{
    public ProcessingChannelValidator()
    {
        When(x => x.EntityId is not null, () =>
            RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithErrorCode(Errors.ClientNotEmpty)
            .Matches(@"^cli_[a-z0-9]{26}$")
            .WithErrorCode(Errors.ClientInvalid));
        
        When(x => x.ClientId is not null, () =>
            RuleFor(x => x.EntityId)
                .NotEmpty()
                .WithErrorCode(Errors.EntityNotEmpty)
                .Matches(@"^ent_[a-z0-9]{26}$")
                .WithErrorCode(Errors.EntityInvalid));
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(Errors.NameRequired);

        RuleFor(x => x.MerchantAccountId)
            .GreaterThan(0)
            .WithErrorCode(Errors.MerchantAccountIdInvalid);
    }

    public static class Errors
    {
        public static string ClientNotEmpty = "client_id_should_not_be_empty";
        public static string ClientInvalid = "client_id_invalid";
        public static string EntityNotEmpty = "entity_id_should_not_be_empty";
        public static string EntityInvalid = "entity_id_invalid";
        public static string NameRequired = "entity_should_not_be_empty";
        public static string MerchantAccountIdInvalid = "entity_account_id_should_be_greater_than_0";
    }
}