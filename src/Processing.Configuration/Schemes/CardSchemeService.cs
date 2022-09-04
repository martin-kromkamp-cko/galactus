using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.Schemes;

internal class CardSchemeService : ICardSchemeService
{
    private readonly IValidator<CardScheme> _cardSchemeValidator;
    private readonly IConfigurationItemRepository<CardScheme> _cardSchemeRepository;

    public CardSchemeService(IConfigurationItemRepository<CardScheme> cardSchemeRepository, IValidator<CardScheme> cardSchemeValidator)
    {
        _cardSchemeRepository = cardSchemeRepository;
        _cardSchemeValidator = cardSchemeValidator;
    }

    public Task<CardScheme?> GetBySchemeName(string schemeName, CancellationToken cancellationToken)
    {
        var cardScheme = _cardSchemeRepository.All()
            .FirstOrDefaultAsync(x => x.Scheme.ToUpper() == schemeName.ToUpper(), cancellationToken);

        return cardScheme;
    }

    public async Task<ServiceResult<CardScheme>> AddAsync(CardScheme cardScheme, CancellationToken cancellationToken)
    {
        var validationResults = await _cardSchemeValidator.ValidateAsync(cardScheme, cancellationToken);
        if (!validationResults.IsValid)
            return ServiceResult<CardScheme>.FromValidationResult(validationResults);
        
        var existingCardScheme = await _cardSchemeRepository
            .All(includeDisabled: true)
            .FirstOrDefaultAsync(x => x.Scheme == cardScheme.Scheme, cancellationToken);
        
        if (existingCardScheme is not null && existingCardScheme.IsActive)
            return ServiceResult<CardScheme>.FromResult(existingCardScheme);

        if (existingCardScheme is not null && !existingCardScheme.IsActive)
        {
            existingCardScheme.ToggleActive();
            await _cardSchemeRepository.UpdateAsync(existingCardScheme, cancellationToken);
            
            return ServiceResult<CardScheme>.FromResult(existingCardScheme);
        }

        var newCardScheme = await _cardSchemeRepository.AddAsync(cardScheme, cancellationToken);
        return ServiceResult<CardScheme>.FromResult(newCardScheme);
    }

    public async Task<ServiceResult<CardScheme>> DisableAsync(CardScheme cardScheme, CancellationToken cancellationToken)
    {
        if (!cardScheme.IsActive)
            return ServiceResult<CardScheme>.FromResult(cardScheme);
        
        cardScheme.ToggleActive();
        var updatedScheme = await _cardSchemeRepository.UpdateAsync(cardScheme, cancellationToken);

        return ServiceResult<CardScheme>.FromResult(updatedScheme);
    }
}