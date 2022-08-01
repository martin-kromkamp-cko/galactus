using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MockQueryable.Moq;
using Moq;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Tests.Schemes;

public class CardSchemeServiceTests
{
    private readonly IValidator<CardScheme> _cardSchemeValidator;
    private readonly IEntityRepository<CardScheme> _cardSchemeRepository;

    private readonly ICardSchemeService _cardSchemeService;

    public CardSchemeServiceTests()
    {
        _cardSchemeRepository = Mock.Of<IEntityRepository<CardScheme>>();
        _cardSchemeValidator = Mock.Of<IValidator<CardScheme>>();

        _cardSchemeService = new CardSchemeService(_cardSchemeRepository, _cardSchemeValidator);
    }
    
    [Fact]
    public async Task GivenInvalidCardSchemeWhenAddingShouldReturnProblem()
    {
        // Given
        var newCardScheme = CardScheme.Create("");

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(newCardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult(new []{ new ValidationFailure("scheme", CardSchemeValidator.Errors.SchemeRequired)}));

        // When
        var response = await _cardSchemeService.AddAsync(newCardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.ValidationError>();
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.AddAsync(newCardScheme, CancellationToken.None),
                Times.Never);
    }

    [Fact]
    public async Task GivenNewCardSchemeWhenAddingShouldAdd()
    {
        // Given
        var newCardScheme = CardScheme.Create("Visa");
        var existingCardSchemes = new CardScheme[] { }.BuildMock();

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(newCardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_cardSchemeRepository)
            .Setup(x => x.All(true))
            .Returns(existingCardSchemes);

        // When
        var response = await _cardSchemeService.AddAsync(newCardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.Success>();
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.AddAsync(newCardScheme, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledCardSchemeWhenAddingShouldEnable()
    {
        // Given
        var newCardScheme = CardScheme.Create("Visa");
        newCardScheme.ToggleActive();
        var existingCardSchemes = new[] { newCardScheme }.BuildMock();

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(newCardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_cardSchemeRepository)
            .Setup(x => x.All(true))
            .Returns(existingCardSchemes);

        // When
        var response = await _cardSchemeService.AddAsync(newCardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.Success>();
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.UpdateAsync(newCardScheme, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenEnabledCardSchemeWhenAddingShouldReturn()
    {
        // Given
        var newCardScheme = CardScheme.Create("Visa");
        var existingCardSchemes = new[] { newCardScheme }.BuildMock();

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(newCardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_cardSchemeRepository)
            .Setup(x => x.All(true))
            .Returns(existingCardSchemes);

        // When
        var response = await _cardSchemeService.AddAsync(newCardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.Success>();
        
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.UpdateAsync(newCardScheme, CancellationToken.None),
                Times.Never);
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.AddAsync(newCardScheme, CancellationToken.None),
                Times.Never);
    }
    
    [Fact]
    public async Task GivenEnabledCardSchemeWhenDisablingShouldUpdate()
    {
        // Given
        var cardScheme = CardScheme.Create("Visa");
        var existingCardSchemes = new[] { cardScheme }.BuildMock();

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(cardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_cardSchemeRepository)
            .Setup(x => x.All(false))
            .Returns(existingCardSchemes);

        // When
        var response = await _cardSchemeService.DisableAsync(cardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.Success>();
        
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.UpdateAsync(cardScheme, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledCardSchemeWhenDisablingShouldReturn()
    {
        // Given
        var cardScheme = CardScheme.Create("Visa");
        cardScheme.ToggleActive();
        var existingCardSchemes = new[] { cardScheme }.BuildMock();

        Mock.Get(_cardSchemeValidator)
            .Setup(x => x.ValidateAsync(cardScheme, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_cardSchemeRepository)
            .Setup(x => x.All(false))
            .Returns(existingCardSchemes);

        // When
        var response = await _cardSchemeService.DisableAsync(cardScheme, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<CardScheme>.Success>();
        
        Mock.Get(_cardSchemeRepository)
            .Verify(x => x.UpdateAsync(cardScheme, CancellationToken.None),
                Times.Never);
    }
}