using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
        var response = await _currencyService.AddAsync(request.To(), cancellationToken);
        if (response.HasErrors())
            return BadRequest(response.Errors);

        return Ok(CurrencyResponse.From(response.Response!));
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
        
        var response = await _currencyService.DisableAsync(currency, cancellationToken);
        if (response.HasErrors())
            return BadRequest(response.Errors);

        return Ok(CurrencyResponse.From(response.Response!));
    }
}