using FluentAssertions;
using FluentValidation;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Tests.Currencies;

public class CurrencyValidatorTests
{
    private readonly IValidator<Currency> _validator;

    public CurrencyValidatorTests()
    {
        _validator = new CurrencyValidator();
    }
    
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("United Kingdom", true)]
    public async Task GivenCountryWhenValidationShouldReturn(string country, bool isValid)
    {
        // Given
        var currency = Currency.Create(country, "Pound Sterling", "GBP", 826);
        
        // When
        var result = await _validator.ValidateAsync(currency, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(CurrencyValidator.Errors.CountryRequired);
        }
    }
    
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("Pound Sterling", true)]
    public async Task GivenNameWhenValidationShouldReturn(string name, bool isValid)
    {
        // Given
        var currency = Currency.Create("United Kingdom", name, "GBP", 826);
        
        // When
        var result = await _validator.ValidateAsync(currency, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(CurrencyValidator.Errors.NameRequired);
        }
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("US", false)]
    [InlineData("USD", true)]
    [InlineData("USDC", false)]
    public async Task GivenCodeWhenValidationShouldReturn(string? code, bool isValid)
    {
        // Given
        var currency = Currency.Create("United Kingdom", "Pound Sterling", code, 826);
        
        // When
        var result = await _validator.ValidateAsync(currency, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(
                code is null or ""
                    ? CurrencyValidator.Errors.CodeRequired
                    : CurrencyValidator.Errors.CodeMustBeOfLength);
        }
    }
    
    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(1000, false)]
    public async Task GivenNumberWhenValidationShouldReturn(int number, bool isValid)
    {
        // Given
        var currency = Currency.Create("United Kingdom", "Pound Sterling", "GBP", number);
        
        // When
        var result = await _validator.ValidateAsync(currency, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(CurrencyValidator.Errors.NumberMustBeBetween);
        }
    }
}