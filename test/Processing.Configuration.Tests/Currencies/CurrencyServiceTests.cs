using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MockQueryable.Moq;
using Moq;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Tests.Currencies;

public class CurrencyServiceTests
{
    private readonly IValidator<Currency> _currencyValidator;
    private readonly IConfigurationItemRepository<Currency> _currencyRepository;

    private readonly ICurrencyService _currencyService;

    public CurrencyServiceTests()
    {
        _currencyRepository = Mock.Of<IConfigurationItemRepository<Currency>>();
        _currencyValidator = Mock.Of<IValidator<Currency>>();

        _currencyService = new CurrencyService(_currencyRepository, _currencyValidator);
    }
    
    [Fact]
    public async Task GivenInvalidCurrencyWhenAddingShouldReturnProblem()
    {
        // Given
        var newCurrency = Currency.Create("Pound Sterling", "GBP", 826);

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult(new []{ new ValidationFailure("name", CurrencyValidator.Errors.NameRequired)}));

        // When
        var response = await _currencyService.AddAsync(newCurrency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.ValidationError>();
        Mock.Get(_currencyRepository)
            .Verify(x => x.AddAsync(newCurrency, CancellationToken.None),
                Times.Never);
    }

    [Fact]
    public async Task GivenNewCurrencyWhenAddingShouldAdd()
    {
        // Given
        var newCurrency = Currency.Create("Pound Sterling", "GBP", 826);
        var existingCurrencies = new Currency[] { }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All(true))
            .Returns(existingCurrencies);

        // When
        var response = await _currencyService.AddAsync(newCurrency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.Success>();
        Mock.Get(_currencyRepository)
            .Verify(x => x.AddAsync(newCurrency, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledCurrencyWhenAddingShouldEnable()
    {
        // Given
        var newCurrency = Currency.Create("Pound Sterling", "GBP", 826);
        newCurrency.ToggleActive();
        var existingCurrencies = new[] { newCurrency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All(true))
            .Returns(existingCurrencies);

        // When
        var response = await _currencyService.AddAsync(newCurrency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.Success>();
        Mock.Get(_currencyRepository)
            .Verify(x => x.UpdateAsync(newCurrency, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenEnabledCurrencyWhenAddingShouldReturn()
    {
        // Given
        var newCurrency = Currency.Create("Pound Sterling", "GBP", 826);
        var existingCurrencies = new[] { newCurrency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All(true))
            .Returns(existingCurrencies);

        // When
        var response = await _currencyService.AddAsync(newCurrency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.Success>();
        
        Mock.Get(_currencyRepository)
            .Verify(x => x.UpdateAsync(newCurrency, CancellationToken.None),
                Times.Never);
        Mock.Get(_currencyRepository)
            .Verify(x => x.AddAsync(newCurrency, CancellationToken.None),
                Times.Never);
    }
    
    [Fact]
    public async Task GivenEnabledCurrencyWhenDisablingShouldUpdate()
    {
        // Given
        var currency = Currency.Create("Pound Sterling", "GBP", 826);
        var existingCurrencies = new[] { currency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(currency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All(false))
            .Returns(existingCurrencies);

        // When
        var response = await _currencyService.DisableAsync(currency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.Success>();
        
        Mock.Get(_currencyRepository)
            .Verify(x => x.UpdateAsync(currency, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledCurrencyWhenDisablingShouldReturn()
    {
        // Given
        var currency = Currency.Create("Pound Sterling", "GBP", 826);
        currency.ToggleActive();
        var existingCurrencies = new[] { currency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(currency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All(false))
            .Returns(existingCurrencies);

        // When
        var response = await _currencyService.DisableAsync(currency, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<Currency>.Success>();
        
        Mock.Get(_currencyRepository)
            .Verify(x => x.UpdateAsync(currency, CancellationToken.None),
                Times.Never);
    }
}
