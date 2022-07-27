using FluentValidation;

namespace Processing.Configuration.Currencies;

public class CurrencyValidator : AbstractValidator<Currency>
{
    public CurrencyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(Errors.CodeRequired)
            .Length(3)
            .WithErrorCode(Errors.CodeMustBeOfLength);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(Errors.NameRequired);

        RuleFor(x => x.Number)
            .ExclusiveBetween(0, 1000)
            .WithErrorCode(Errors.NumberMustBeBetween);
    }

    public static class Errors
    {
        public static string CodeRequired = "code_required";
        public static string CodeMustBeOfLength = "code_must_be_of_length_3";
        public static string CountryRequired = "country_required";
        public static string NameRequired = "name_required";
        public static string NumberMustBeBetween = "number_must_be_between_0_and_1000_exclusive";
    }
}