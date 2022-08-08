namespace Processing.Configuration.ProcessingChannels;

public interface IProcessingChannelService
{
    Task<ProcessingChannel?> GetByExternalId(string externalId, CancellationToken cancellationToken);

    Task<ServiceResult<ProcessingChannel>> AddAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken);
    
    Task<ServiceResult<ProcessingChannel>> UpdateAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken);

    Task<ServiceResult<ProcessingChannel>> DisableAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken);
}