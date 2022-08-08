using Microsoft.EntityFrameworkCore;

namespace Processing.Configuration.Infra.Data.Processing;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    private readonly ProcessingContext _dbContext;

    public EntityRepository(ProcessingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> All(bool includeDisabled = false)
    {
        return includeDisabled 
            ? _dbContext.Set<TEntity>().IgnoreQueryFilters() 
            : _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var newEntity = await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newEntity.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}