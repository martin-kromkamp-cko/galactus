using FluentValidation;

namespace Processing.Configuration.MerchantCategoryCodes;

public class MerchantCategoryCodeValidator : AbstractValidator<MerchantCategoryCode>
{
    public MerchantCategoryCodeValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode(Errors.TitleRequired);
        
        RuleFor(x => x.Code)
            .InclusiveBetween(1000, 9999)
            .WithErrorCode(Errors.CodeMustBeBetween);
    }

    public static class Errors
    {
        public static string TitleRequired = "title_required";
        public static string CodeMustBeBetween = "code_must_be_between_1000_and_9999_inclusive";
        
    }
}