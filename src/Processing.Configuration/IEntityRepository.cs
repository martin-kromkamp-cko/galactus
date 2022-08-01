namespace Processing.Configuration;

public interface IEntityRepository<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> All(bool includeDisabled = false);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}