using Microsoft.AspNetCore.Mvc;
using Processing.Configuration.ProcessingChannels;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

[Route("api/processing-channels")]
public class ProcessingChannelController : ControllerBase
{
    private readonly IProcessingChannelService _processingChannelService;
    private readonly IProcessingChannelMapper _processingChannelMapper;

    public ProcessingChannelController(IProcessingChannelService processingChannelService, IProcessingChannelMapper processingChannelMapper)
    {
        _processingChannelService = processingChannelService;
        _processingChannelMapper = processingChannelMapper;
    }

    [HttpGet("{processingChannelId}")]
    public async Task<IActionResult> GetByChannelIdAsync(string processingChannelId,
        CancellationToken cancellationToken)
    {
        var processingChannel = await _processingChannelService.GetByExternalId(processingChannelId, cancellationToken);

        return processingChannel is null
            ? NotFound()
            : Ok(ProcessingChannelResponse.From(processingChannel));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProcessingChannelAsync([FromBody] ProcessingChannelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _processingChannelMapper.MapAsync(request, cancellationToken);
        if (result.Errors.Any())
            return BadRequest(result.Errors);
        
        var newProcessingChannel = await _processingChannelService.AddAsync(result.ProcessingChannel!, cancellationToken);
        return newProcessingChannel switch
        {
            ServiceResult<ProcessingChannel>.Success success => Ok(ProcessingChannelResponse.From(success.Result)),
            ServiceResult<ProcessingChannel>.ValidationError error => BadRequest(error.Errors.ToList()),
            ServiceResult<ProcessingChannel>.InternalError => new StatusCodeResult(500),
        };
    }
}