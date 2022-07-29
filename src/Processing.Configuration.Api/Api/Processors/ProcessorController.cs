using Microsoft.AspNetCore.Mvc;
using Processing.Configuration.Currencies;
using Processing.Configuration.MerchantCategoryCodes;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;
using Processing.Configuration.Schemes;

namespace Processing.Configuration.Api.Api.Processors;

[Route("api/processors")]
public class ProcessorController : ControllerBase
{
    private readonly IProcessorService _processorService;
    private readonly IProcessingChannelService _processingChannelService;

    private readonly ICurrencyService _currencyService;
    private readonly ICardSchemeService _cardSchemeService;
    private readonly IMerchantCategoryCodeService _merchantCategoryCodeService;

    public ProcessorController(IProcessorService processorService, 
        IProcessingChannelService processingChannelService, 
        ICurrencyService currencyService, 
        ICardSchemeService cardSchemeService,
        IMerchantCategoryCodeService merchantCategoryCodeService)
    {
        _processorService = processorService;
        _processingChannelService = processingChannelService;
        _currencyService = currencyService;
        _cardSchemeService = cardSchemeService;
        _merchantCategoryCodeService = merchantCategoryCodeService;
    }

    [HttpGet("{processorId}")]
    public async Task<IActionResult> GetProcessorById(string processorId, CancellationToken cancellationToken)
    {
        var processor = await _processorService.GetByExternalId(processorId, cancellationToken);

        return processor is null
            ? NotFound()
            : Ok(ProcessorResponse.From(processor));
    }

    [HttpPost, Route("api/processing-channels/{processingChannelId}/processors")]
    public async Task<IActionResult> CreateProcessorForProcessingChannel([FromRoute] string processingChannelId, [FromBody] ProcessorRequest request,
        CancellationToken cancellationToken)
    {
        var processingChannel = await _processingChannelService.GetByExternalId(processingChannelId, cancellationToken);
        if (processingChannel is null)
            return NotFound();

        var processor = await request.ToProcessorAsync(processingChannel, _currencyService, _cardSchemeService, _merchantCategoryCodeService, cancellationToken);
        if (processor is null)
            return BadRequest();

        var newProcessor = await _processorService.AddAsync(processor, cancellationToken);

        return newProcessor switch
        {
            ServiceResult<Processor>.Success success => Ok(ProcessorResponse.From(success.Result)),
            ServiceResult<Processor>.ValidationError error => BadRequest(error.Errors.ToList()),
            ServiceResult<Processor>.InternalError => new StatusCodeResult(500),
        };
    }
}