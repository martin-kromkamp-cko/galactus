namespace Processing.Configuration;

public interface IEntityRepository<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> All();

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}