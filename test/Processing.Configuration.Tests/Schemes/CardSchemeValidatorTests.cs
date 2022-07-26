using FluentAssertions;
using FluentValidation;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Tests.Schemes;

public class CardSchemeValidatorTests
{
    private readonly IValidator<CardScheme> _validator;

    public CardSchemeValidatorTests()
    {
        _validator = new CardSchemeValidator();
    }
    
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("Visa", true)]
    public async Task GivenSchemeWhenValidationShouldReturn(string schemeName, bool isValid)
    {
        // Given
        var cardScheme = CardScheme.Create(schemeName);
        
        // When
        var result = await _validator.ValidateAsync(cardScheme, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(CardSchemeValidator.Errors.SchemeRequired);
        }
    }
}