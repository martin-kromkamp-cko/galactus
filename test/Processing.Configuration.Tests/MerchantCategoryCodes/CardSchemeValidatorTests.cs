using FluentAssertions;
using FluentValidation;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Tests.MerchantCategoryCodes;

public class MerchantCategoryCodeValidatorTests
{
    private readonly IValidator<MerchantCategoryCode> _validator;

    public MerchantCategoryCodeValidatorTests()
    {
        _validator = new MerchantCategoryCodeValidator();
    }
    
    [Theory]
    [InlineData(1, false)]
    [InlineData(999, false)]
    [InlineData(1000, true)]
    [InlineData(9999, true)]
    [InlineData(10000, false)]
    public async Task GivenCodeWhenValidationShouldReturn(int code, bool isValid)
    {
        // Given
        var mcc = MerchantCategoryCode.Create("Title", code);
        
        // When
        var result = await _validator.ValidateAsync(mcc, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(MerchantCategoryCodeValidator.Errors.CodeMustBeBetween);
        }
    }
    
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("Title", true)]
    public async Task GivenTitleWhenValidationShouldReturn(string title, bool isValid)
    {
        // Given
        var mcc = MerchantCategoryCode.Create(title, 1000);
        
        // When
        var result = await _validator.ValidateAsync(mcc, CancellationToken.None);
        
        // Then
        result.IsValid.Should().Be(isValid);
        if (!result.IsValid)
        {
            result.Errors.First().ErrorCode.Should().Be(MerchantCategoryCodeValidator.Errors.TitleRequired);
        }
    }
}