using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.MerchantCategoryCodes;

public class MerchantCategoryCodeService : IMerchantCategoryCodeService
{
    private readonly IValidator<MerchantCategoryCode> _validator;
    private readonly IConfigurationItemRepository<MerchantCategoryCode> _merchantCategoryCodeRepository;

    public MerchantCategoryCodeService(IValidator<MerchantCategoryCode> validator, IConfigurationItemRepository<MerchantCategoryCode> merchantCategoryCodeRepository)
    {
        _validator = validator;
        _merchantCategoryCodeRepository = merchantCategoryCodeRepository;
    }

    public Task<MerchantCategoryCode?> GetByCodeAsync(int code, CancellationToken cancellationToken)
    {
        return _merchantCategoryCodeRepository.All().FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }

    public async Task<ServiceResult<MerchantCategoryCode>> AddAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(merchantCategoryCode, cancellationToken);
        if (!validationResults.IsValid)
            return ServiceResult<MerchantCategoryCode>.FromValidationResult(validationResults);
        
        var existingMcc = await _merchantCategoryCodeRepository
            .All(includeDisabled: true)
            .FirstOrDefaultAsync(x => x.Code == merchantCategoryCode.Code, cancellationToken);
        
        if (existingMcc is not null && existingMcc.IsActive)
            return ServiceResult<MerchantCategoryCode>.FromResult(existingMcc);

        if (existingMcc is not null && !existingMcc.IsActive)
        {
            existingMcc.ToggleActive();
            await _merchantCategoryCodeRepository.UpdateAsync(existingMcc, cancellationToken);
            
            return ServiceResult<MerchantCategoryCode>.FromResult(existingMcc);
        }

        var newMcc = await _merchantCategoryCodeRepository.AddAsync(merchantCategoryCode, cancellationToken);
        return ServiceResult<MerchantCategoryCode>.FromResult(newMcc);
    }

    public async Task<ServiceResult<MerchantCategoryCode>> DisableAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken)
    {
        if (!merchantCategoryCode.IsActive)
            return ServiceResult<MerchantCategoryCode>.FromResult(merchantCategoryCode);
        
        merchantCategoryCode.ToggleActive();
        var updatedMcc = await _merchantCategoryCodeRepository.UpdateAsync(merchantCategoryCode, cancellationToken);

        return ServiceResult<MerchantCategoryCode>.FromResult(updatedMcc);
    }
}