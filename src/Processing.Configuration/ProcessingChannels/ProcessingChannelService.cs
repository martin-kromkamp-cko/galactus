using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.ProcessingChannels;

public class ProcessingChannelService : IProcessingChannelService
{
    private readonly IValidator<ProcessingChannel> _validator;
    private readonly IConfigurationItemRepository<ProcessingChannel> _processingChannelRepository;

    public ProcessingChannelService(IValidator<ProcessingChannel> validator, IConfigurationItemRepository<ProcessingChannel> processingChannelRepository)
    {
        _validator = validator;
        _processingChannelRepository = processingChannelRepository;
    }

    public Task<ProcessingChannel?> GetByExternalId(string externalId, CancellationToken cancellationToken)
    {
        return _processingChannelRepository.All()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);
    }

    public async Task<ServiceResult<ProcessingChannel>> AddAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(processingChannel, cancellationToken);
        if (!validationResults.IsValid)
            return ServiceResult<ProcessingChannel>.FromValidationResult(validationResults);

        var newProcessingChannel = await _processingChannelRepository.AddAsync(processingChannel, cancellationToken);
        return ServiceResult<ProcessingChannel>.FromResult(newProcessingChannel);
    }

    public async Task<ServiceResult<ProcessingChannel>> UpdateAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(processingChannel, cancellationToken);
        if (!validationResults.IsValid)
            return ServiceResult<ProcessingChannel>.FromValidationResult(validationResults);

        var updatedProcessingChannel = await _processingChannelRepository.UpdateAsync(processingChannel, cancellationToken);
        return ServiceResult<ProcessingChannel>.FromResult(updatedProcessingChannel);
    }

    public async Task<ServiceResult<ProcessingChannel>> DisableAsync(ProcessingChannel processingChannel, CancellationToken cancellationToken)
    {
        if (!processingChannel.IsActive)
            return ServiceResult<ProcessingChannel>.FromResult(processingChannel);
        
        processingChannel.ToggleActive();
        var updatedProcessingChannel = await _processingChannelRepository.UpdateAsync(processingChannel, cancellationToken);

        return ServiceResult<ProcessingChannel>.FromResult(updatedProcessingChannel);
    }
}