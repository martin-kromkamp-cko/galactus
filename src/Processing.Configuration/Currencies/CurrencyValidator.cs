using FluentValidation;

namespace Processing.Configuration.Currencies;

public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(x => x.Code)
            .Length(3)
            .WithErrorCode(Errors.CodeMustBeOfLength);

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithErrorCode(Errors.CountryRequired);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(Errors.NameRequired);

        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithErrorCode(Errors.NumberGreater);
    }

    public static class Errors
    {
        public static string CodeMustBeOfLength = "code_must_be_of_length_3";
        public static string CountryRequired = "country_required";
        public static string NameRequired = "name_required";
        public static string NumberGreater = "number_must_be_greater_than_0";
    }
}