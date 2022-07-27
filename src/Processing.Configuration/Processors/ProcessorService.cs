using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.Processors;

public class ProcessorService : IProcessorService
{
    private readonly IValidator<Processor> _validator;
    private readonly IEntityRepository<Processor> _processorRepository;

    public ProcessorService(IValidator<Processor> validator, IEntityRepository<Processor> processorRepository)
    {
        _validator = validator;
        _processorRepository = processorRepository;
    }

    public Task<Processor?> GetByExternalId(string externalId, CancellationToken cancellationToken)
    {
        return _processorRepository.All().FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);
    }

    public async Task<ServiceResult<Processor>> AddAsync(Processor processor, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(processor, cancellationToken);
        if (!validationResults.IsValid)
            return ServiceResult<Processor>.FromValidationResult(validationResults);

        var newProcessor = await _processorRepository.AddAsync(processor, cancellationToken);
        return ServiceResult<Processor>.FromResult(newProcessor);
    }

    public async Task<ServiceResult<Processor>> DisableAsync(Processor processor, CancellationToken cancellationToken)
    {
        if (!processor.IsActive)
            return ServiceResult<Processor>.FromResult(processor);
        
        processor.ToggleActive();
        var updatedScheme = await _processorRepository.UpdateAsync(processor, cancellationToken);

        return ServiceResult<Processor>.FromResult(updatedScheme);
    }
}