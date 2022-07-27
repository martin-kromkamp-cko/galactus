namespace Processing.Configuration.Infra.Data.Processing;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    private readonly ProcessingContext _dbContext;

    public EntityRepository(ProcessingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> All()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var newCurrency = await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newCurrency.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}