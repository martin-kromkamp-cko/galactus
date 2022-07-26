using FluentValidation;

namespace Processing.Configuration.Schemes;

public class CardSchemeValidator : AbstractValidator<CardScheme>
{
    public CardSchemeValidator()
    {
        RuleFor(x => x.Scheme)
            .NotEmpty()
            .WithErrorCode(Errors.SchemeRequired);
    }

    public static class Errors
    {
        public static string SchemeRequired = "scheme_required";
    }
}