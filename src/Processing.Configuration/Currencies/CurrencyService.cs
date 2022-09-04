using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.Currencies;

internal class CurrencyService : ICurrencyService
{
    private readonly IValidator<Currency> _currencyValidator;
    private readonly IConfigurationItemRepository<Currency> _currencyRepository;

    public CurrencyService(IConfigurationItemRepository<Currency> currencyRepository, IValidator<Currency> currencyValidator)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
    }

    public async Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository
            .All()
            .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);

        return currency;
    }

    public async Task<IDictionary<string, Currency?>> GetByCodesAsync(IEnumerable<string> codes, CancellationToken cancellationToken)
    {
        var currencies = await _currencyRepository.All().Where(c => codes.Contains(c.Code))
            .ToListAsync(cancellationToken);

        return codes.ToDictionary(
            key => key, 
            value => currencies.FirstOrDefault(c => c.Code == value)
        );
    }

    public async Task<Currency?> GetByExternalId(string externalId, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository
            .All()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);

        return currency;
    }

    public async Task<ServiceResult<Currency>> AddAsync(Currency currency, CancellationToken cancellationToken)
    {
        var validationResult = await _currencyValidator.ValidateAsync(currency, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ServiceResult<Currency>.FromValidationResult(validationResult);
        }

        // If currency already exists but is inactive only activated it again
        var existingCurrency = await _currencyRepository
            .All(includeDisabled: true)
            .FirstOrDefaultAsync(x => x.Code == currency.Code, cancellationToken);
        
        if (existingCurrency is not null)
        {
            if (existingCurrency.IsActive)
            {
                return ServiceResult<Currency>.FromResult(currency);
            }
            
            existingCurrency.ToggleActive();
            await _currencyRepository.UpdateAsync(existingCurrency, cancellationToken);

            return ServiceResult<Currency>.FromResult(currency);
        }
        
        var newCurrency = await _currencyRepository.AddAsync(currency, cancellationToken);
        return ServiceResult<Currency>.FromResult(newCurrency);
    }

    public async Task<ServiceResult<Currency>> DisableAsync(Currency currency, CancellationToken cancellationToken)
    {
        if (!currency.IsActive)
            return ServiceResult<Currency>.FromResult(currency);
        
        currency.ToggleActive();
        var updatedCurrency = await _currencyRepository.UpdateAsync(currency, cancellationToken);

        return ServiceResult<Currency>.FromResult(updatedCurrency);
    }
}