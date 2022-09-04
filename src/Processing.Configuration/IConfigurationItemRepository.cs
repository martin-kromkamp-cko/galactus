namespace Processing.Configuration;

public interface IConfigurationItemRepository<TEntity> where TEntity : ConfigurationItemBase
{
    IQueryable<TEntity> All(bool includeDisabled = false);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}