using Microsoft.AspNetCore.Mvc;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Api.Api.MerchantCategoryCodes;

[Route("api/merchant-category-codes")]
public class MerchantCategoryCodeController : ControllerBase
{
    private readonly IMerchantCategoryCodeService _merchantCategoryCodeService;

    public MerchantCategoryCodeController(IMerchantCategoryCodeService merchantCategoryCodeService)
    {
        _merchantCategoryCodeService = merchantCategoryCodeService;
    }

    [HttpGet("{code:int}")]
    public async Task<IActionResult> GetByCodeAsync(int code, CancellationToken cancellationToken)
    {
        var mcc = await _merchantCategoryCodeService.GetByCodeAsync(code, cancellationToken);

        return mcc is null
            ? NotFound()
            : Ok(MerchantCategoryCodeResponse.From(mcc));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMerchantCategoryCodeAsync([FromBody] MerchantCategoryCodeRequest request,
        CancellationToken cancellationToken)
    {
        var newMcc = await _merchantCategoryCodeService.AddAsync(request.To(), cancellationToken);

        return newMcc switch
        {
            ServiceResult<MerchantCategoryCode>.Success success => Ok(MerchantCategoryCodeResponse.From(success.Result)),
            ServiceResult<MerchantCategoryCode>.ValidationError error => BadRequest(error.Errors),
            ServiceResult<MerchantCategoryCode>.InternalError => new StatusCodeResult(500),
        };
    }
    
    [HttpDelete("{code:int}")]
    public async Task<IActionResult> CreateMerchantCategoryCodeAsync(int code, CancellationToken cancellationToken)
    {
        var mcc = await _merchantCategoryCodeService.GetByCodeAsync(code, cancellationToken);
        if (mcc is null)
            return Accepted();

        var disabledMcc = await _merchantCategoryCodeService.DisableAsync(mcc, cancellationToken);

        return disabledMcc switch
        {
            ServiceResult<MerchantCategoryCode>.Success success => Ok(MerchantCategoryCodeResponse.From(success.Result)),
            ServiceResult<MerchantCategoryCode>.ValidationError error => BadRequest(error.Errors),
            ServiceResult<MerchantCategoryCode>.InternalError => new StatusCodeResult(500),
        };
    }
}