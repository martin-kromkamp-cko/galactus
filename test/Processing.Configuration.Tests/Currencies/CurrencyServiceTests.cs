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
    private readonly ICurrencyRepository _currencyRepository;

    private readonly ICurrencyService _currencyService;

    public CurrencyServiceTests()
    {
        _currencyRepository = Mock.Of<ICurrencyRepository>();
        _currencyValidator = Mock.Of<IValidator<Currency>>();

        _currencyService = new CurrencyService(_currencyRepository, _currencyValidator);
    }
    
    [Fact]
    public async Task GivenInvalidCurrencyWhenAddingShouldReturnProblem()
    {
        // Given
        var newCurrency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);

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
        var newCurrency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);
        var existingCurrencies = new Currency[] { }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All())
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
        var newCurrency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);
        newCurrency.ToggleActive();
        var existingCurrencies = new[] { newCurrency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All())
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
        var newCurrency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);
        var existingCurrencies = new[] { newCurrency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(newCurrency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All())
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
        var currency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);
        var existingCurrencies = new[] { currency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(currency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All())
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
        var currency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", 826);
        currency.ToggleActive();
        var existingCurrencies = new[] { currency }.BuildMock();

        Mock.Get(_currencyValidator)
            .Setup(x => x.ValidateAsync(currency, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_currencyRepository)
            .Setup(x => x.All())
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

public static class MockExtensions
{
    public static void SetupIQueryable<T>(this Mock<T> mock, IQueryable queryable)
        where T: class, IQueryable
    {
        mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
        mock.Setup(r => r.Provider).Returns(queryable.Provider);
        mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
        mock.Setup(r => r.Expression).Returns(queryable.Expression);
    }
}