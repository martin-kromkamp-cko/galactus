using Microsoft.AspNetCore.Mvc;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Schemes;

[Route("api/card-schemes")]
public class CardSchemeController : ControllerBase
{
    private readonly ICardSchemeService _cardSchemeService;

    public CardSchemeController(ICardSchemeService cardSchemeService)
    {
        _cardSchemeService = cardSchemeService;
    }

    [HttpGet("{scheme}")]
    public async Task<IActionResult> GetBySchemeAsync(string scheme, CancellationToken cancellationToken)
    {
        var cardScheme = await _cardSchemeService.GetBySchemeName(scheme, cancellationToken);

        return cardScheme is null
            ? NotFound()
            : Ok(CardSchemeResponse.From(cardScheme));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchemeAsync([FromBody] CardSchemeRequest cardSchemeRequest, CancellationToken cancellationToken)
    {
        var newCardScheme = await _cardSchemeService.AddAsync(cardSchemeRequest.To(), cancellationToken);

        return newCardScheme switch
        {
            ServiceResult<CardScheme>.Success succes => Ok(CardSchemeResponse.From(succes.Result)),
            ServiceResult<CardScheme>.ValidationError error => BadRequest(error.Errors),
            ServiceResult<CardScheme>.InternalError => new StatusCodeResult(500),
        };
    }

    [HttpDelete("{scheme}")]
    public async Task<IActionResult> DisableSchemeAsync(string scheme, CancellationToken cancellationToken)
    {
        var cardScheme = await _cardSchemeService.GetBySchemeName(scheme, cancellationToken);
        if (cardScheme is null)
            return Accepted();

        var disabledCardScheme = await _cardSchemeService.DisableAsync(cardScheme, cancellationToken);
        
        return disabledCardScheme switch
        {
            ServiceResult<CardScheme>.Success succes => Ok(CardSchemeResponse.From(succes.Result)),
            ServiceResult<CardScheme>.ValidationError error => BadRequest(error.Errors),
            ServiceResult<CardScheme>.InternalError => new StatusCodeResult(500),
        };
    }
}