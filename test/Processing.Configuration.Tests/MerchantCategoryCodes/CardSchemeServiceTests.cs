using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MockQueryable.Moq;
using Moq;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Tests.MerchantCategoryCodes;

public class MerchantCategoryCodeServiceTests
{
    private readonly IValidator<MerchantCategoryCode> _merchantCategoryCodeValidator;
    private readonly IEntityRepository<MerchantCategoryCode> _merchantCategoryCodeRepository;

    private readonly IMerchantCategoryCodeService _merchantCategoryCodeService;

    public MerchantCategoryCodeServiceTests()
    {
        _merchantCategoryCodeRepository = Mock.Of<IEntityRepository<MerchantCategoryCode>>();
        _merchantCategoryCodeValidator = Mock.Of<IValidator<MerchantCategoryCode>>();

        _merchantCategoryCodeService = new MerchantCategoryCodeService(_merchantCategoryCodeValidator, _merchantCategoryCodeRepository);
    }
    
    [Fact]
    public async Task GivenInvalidMerchantCategoryCodeWhenAddingShouldReturnProblem()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("", 1000);

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult(new []{ new ValidationFailure("title", MerchantCategoryCodeValidator.Errors.TitleRequired)}));

        // When
        var response = await _merchantCategoryCodeService.AddAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.ValidationError>();
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.AddAsync(newMcc, CancellationToken.None),
                Times.Never);
    }

    [Fact]
    public async Task GivenNewMerchantCategoryCodeWhenAddingShouldAdd()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("Title", 1000);
        var existingMccs = new MerchantCategoryCode[] { }.BuildMock();

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_merchantCategoryCodeRepository)
            .Setup(x => x.All(true))
            .Returns(existingMccs);

        // When
        var response = await _merchantCategoryCodeService.AddAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.Success>();
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.AddAsync(newMcc, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledMerchantCategoryCodeWhenAddingShouldEnable()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("Title", 1000);
        newMcc.ToggleActive();
        var existingMccs = new[] { newMcc }.BuildMock();

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_merchantCategoryCodeRepository)
            .Setup(x => x.All(true))
            .Returns(existingMccs);

        // When
        var response = await _merchantCategoryCodeService.AddAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.Success>();
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.UpdateAsync(newMcc, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenEnabledMerchantCategoryCodeWhenAddingShouldReturn()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("Title", 1000);
        var existingMccs = new[] { newMcc }.BuildMock();

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_merchantCategoryCodeRepository)
            .Setup(x => x.All(true))
            .Returns(existingMccs);

        // When
        var response = await _merchantCategoryCodeService.AddAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.Success>();
        
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.UpdateAsync(newMcc, CancellationToken.None),
                Times.Never);
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.AddAsync(newMcc, CancellationToken.None),
                Times.Never);
    }
    
    [Fact]
    public async Task GivenEnabledMerchantCategoryCodeWhenDisablingShouldUpdate()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("Title", 1000);
        var existingMccs = new[] { newMcc }.BuildMock();

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_merchantCategoryCodeRepository)
            .Setup(x => x.All(false))
            .Returns(existingMccs);

        // When
        var response = await _merchantCategoryCodeService.DisableAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.Success>();
        
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.UpdateAsync(newMcc, CancellationToken.None),
                Times.Once);
    }
    
    [Fact]
    public async Task GivenDisabledMerchantCategoryCodeWhenDisablingShouldReturn()
    {
        // Given
        var newMcc = MerchantCategoryCode.Create("Title", 1000);
        newMcc.ToggleActive();
        var existingMccs = new[] { newMcc }.BuildMock();

        Mock.Get(_merchantCategoryCodeValidator)
            .Setup(x => x.ValidateAsync(newMcc, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Mock.Get(_merchantCategoryCodeRepository)
            .Setup(x => x.All(false))
            .Returns(existingMccs);

        // When
        var response = await _merchantCategoryCodeService.DisableAsync(newMcc, CancellationToken.None);

        // Then
        response.Should().BeOfType<ServiceResult<MerchantCategoryCode>.Success>();
        
        Mock.Get(_merchantCategoryCodeRepository)
            .Verify(x => x.UpdateAsync(newMcc, CancellationToken.None),
                Times.Never);
    }
}