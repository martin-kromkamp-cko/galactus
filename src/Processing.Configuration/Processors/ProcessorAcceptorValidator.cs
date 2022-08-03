using FluentValidation;

namespace Processing.Configuration.Processors;

public class ProcessorAcceptorValidator : AbstractValidator<ProcessorAcceptor>
{
    public ProcessorAcceptorValidator()
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.SchemeMerchantId)
            .NotEmpty().WithErrorCode(Errors.SchemeMerchantIdRequired);

        RuleFor(x => x.Name)
            .NotEmpty().WithErrorCode(Errors.AcceptorNameRequired);

        RuleFor(x => x.Country)
            .NotEmpty().WithErrorCode(Errors.CountryRequired)
            .Matches(@"^[a-zA-Z]{2}$") // TODO: 2 char iso-country-code < move to table
            .WithErrorCode(Errors.AcceptorCountryInvalid);

        RuleFor(x => x.City)
            .NotEmpty().WithErrorCode(Errors.AcceptorCityRequired);
    }

    public static class Errors
    {
        public static readonly string SchemeMerchantIdRequired = "scheme_merchant_id_required";
        public static readonly string AcceptorNameRequired = "acceptor_name_required";
        public static readonly string CountryRequired = "country_country_required";
        public static readonly string AcceptorCountryInvalid = "country_code_invalid";
        public static readonly string AcceptorCityRequired = "acceptor_city_required";
    }
}