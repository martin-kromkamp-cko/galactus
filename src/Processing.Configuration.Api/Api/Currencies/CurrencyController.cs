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
    
    [HttpPost]
    public async Task<IActionResult> CreateCurrency([FromBody]CurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = await _currencyService.AddAsync(request.To(), cancellationToken);

        return Ok(CurrencyResponse.From(currency));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetCurrencyByCode(string code, CancellationToken cancellationToken)
    {
        var currency = await _currencyService.GetByCodeAsync(code, cancellationToken);

        return currency is null
            ? NotFound()
            : Ok(CurrencyResponse.From(currency));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DisableCurrency(string code, CancellationToken cancellationToken)
    {
        var currency = await _currencyService.GetByCodeAsync(code, cancellationToken);
        if (currency is null)
            return NotFound();
        
        var deletedCurrency = await _currencyService.DisableAsync(currency, cancellationToken);

        return Ok(CurrencyResponse.From(deletedCurrency));
    }
}