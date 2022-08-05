using Processing.Configuration.Api.Api.Processors;
using Processing.Configuration.ProcessingChannels;
using Processing.Configuration.Processors;

namespace Processing.Configuration.Api.Api.ProcessingChannels;

public interface IProcessingChannelMapper
{
    Task<(ProcessingChannel? ProcessingChannel, List<string> Errors)> MapAsync(ProcessingChannelRequest request, CancellationToken cancellationToken);
}

public class ProcessingChannelMapper : IProcessingChannelMapper
{
    private readonly IProcessorMapper _processorMapper;

    public ProcessingChannelMapper(IProcessorMapper processorMapper)
    {
        _processorMapper = processorMapper;
    }

    public async Task<(ProcessingChannel? ProcessingChannel, List<string> Errors)> MapAsync(ProcessingChannelRequest request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var processors = new List<Processor>();

        var processingChannel = request.To();

        foreach (var processor in request.Processors)
        {
            var processorResult = await _processorMapper.MapToAsync(processor, processingChannel, cancellationToken);
            
            if (processorResult.Errors.Any())
                errors.AddRange(errors);
            
            if (processorResult.Processor is not null)
                processors.Add(processorResult.Processor);
        }

        processingChannel.Processors = processors;
        return (processingChannel, errors);
    }
}