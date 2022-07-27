namespace Processing.Configuration.Processors;

public interface IProcessorService
{
    Task<Processor?> GetByExternalId(string externalId, CancellationToken cancellationToken);

    Task<ServiceResult<Processor>> AddAsync(Processor processor, CancellationToken cancellationToken);

    Task<ServiceResult<Processor>> DisableAsync(Processor processor, CancellationToken cancellationToken);
}
