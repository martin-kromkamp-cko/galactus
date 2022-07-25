using FluentValidation;
using Moq;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Tests.Currencies;

public class CurrencyServiceTests
{
    private readonly IValidator<Currency> _currencyValidator;
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyServiceTests()
    {
        _currencyRepository = Mock.Of<ICurrencyRepository>();
        _currencyValidator = Mock.Of<IValidator<Currency>>();
    }
    
    
}