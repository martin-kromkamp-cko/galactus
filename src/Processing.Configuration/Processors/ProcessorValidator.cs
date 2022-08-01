using FluentValidation;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Processors;

public class ProcessorValidator : AbstractValidator<Processor>
{
    public ProcessorValidator(IMerchantCategoryCodeService mccService, ICurrencyService currencyService)
    {
        RuleFor(x => x.AcquirerId)
            .NotNull()
            .WithErrorCode(Errors.AcquirerIdRequired)
            .Matches(@"^[a-z0-9_\-]+$")
            .WithErrorCode(Errors.InvalidAcquirer);

        RuleFor(x => x.MerchantCategoryCode)
            .NotNull()
            .WithErrorCode(Errors.MccRequired)
            .MustAsync(async (mcc, _) => await mccService.GetByCodeAsync(mcc.Code, CancellationToken.None) is not null)
            .When(mcc => mcc is not null)
            .WithErrorCode(Errors.InvalidMcc);

        RuleForEach(x => x.Currencies)
            .NotEmpty()
            .WithErrorCode(Errors.CurrenciesRequired)
            .MustAsync(async (currency, _) => await currencyService.GetByCodeAsync(currency.Code, CancellationToken.None) is not null)
            .When(currency => currency is not null)
            .WithErrorCode(Errors.InvalidCurrencies);

        // RuleFor(x => x.Acceptor)
        //     .NotNull().WithErrorCode(ErrorCodes.AcceptorRequired)
        //     .SetValidator(new ProcessorAcceptorValidator());

        RuleFor(x => x.Services)
            .Must(BeServiceUnique)
            .When(x => x.Services is not null)
            .WithErrorCode(Errors.DuplicateService);

        RuleForEach(x => x.Services)
            .SetValidator(new ProcessorServiceValidator());
        
        RuleFor(x => x.Mode)
            .IsInEnum()
            .WithErrorCode(Errors.ProcessorModeInvalid);

        // RuleFor(x => x.Features)
        //     .SetValidator(new FeaturesCollectionValidator(isProcessor: true))
        //     .When(x => x.Features != null && x.Features.Any());
    }

    private static bool BeServiceUnique(IEnumerable<CkoService> services)
    {
        return services.Select(a => a.Type + a.Version).Distinct().Count() == services.Count();
    }

    public static class Errors
    {
        public static string AcquirerIdRequired = "acquirer_id_required";
        public static string InvalidAcquirer = "invalid_acquirer_id";
        public static string MccRequired = "merchant_category_code_required";
        public static string InvalidMcc = "invalid_merchant_category_code";
        public static string CurrenciesRequired = "currencies_required";
        public static string InvalidCurrencies = "invalid_currencies";
        public static string DuplicateService = "duplicate_service";
        public static string ProcessorModeInvalid = "processor_mode_invalid";
    }
}