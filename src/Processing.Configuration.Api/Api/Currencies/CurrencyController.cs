using Microsoft.AspNetCore.Mvc;
using Processing.Configuration.Currencies;

namespace Processing.Configuration.Api.Api.Currencies;

[Route("api/currencies")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetCurrencyByCode(string code, CancellationToken cancellationToken)
    {
        var currency = await _currencyService.GetByCodeAsync(code, cancellationToken);

        return currency is null
            ? NotFound()
            : Ok(CurrencyResponse.From(currency));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCurrency([FromBody]CurrencyRequest request, CancellationToken cancellationToken)
    {
        var response = await _currencyService.AddAsync(request.To(), cancellationToken);
        return response switch
        {
            ServiceResult<Currency>.Success currency => Ok(CurrencyResponse.From(currency.Result)),
            ServiceResult<Currency>.ValidationError validation => BadRequest(validation.Errors.Select(e => e.Error)),
            ServiceResult<Currency>.InternalError error => Problem()
        };
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DisableCurrency(string code, CancellationToken cancellationToken)
    {
        var existingCurrency = await _currencyService.GetByCodeAsync(code, cancellationToken);
        if (existingCurrency is null)
            return NotFound();
        
        var response = await _currencyService.DisableAsync(existingCurrency, cancellationToken);
        return response switch
        {
            ServiceResult<Currency>.Success currency => Ok(CurrencyResponse.From(currency.Result)),
            ServiceResult<Currency>.ValidationError validation => BadRequest(validation.Errors.Select(e => e.Error)),
            ServiceResult<Currency>.InternalError error => Problem()
        };
    }
}