using IKao.WebAnalytics.Domain.Abstraction;

namespace IKao.WebAnalytics.Infrastructure;

public class BaseEFRepository : IRepository
{
    public TEntity InsertOrUpdate<TEntity>(TEntity entity) where TEntity : class
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertOrUpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        throw new NotImplementedException();
    }

    public void InsertOrUpdateBulk<TEntity>(TEntity[] entity) where TEntity : class
    {
        throw new NotImplementedException();
    }

    public Task InsertOrUpdateBulkAsync<TEntity>(TEntity[] entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        throw new NotImplementedException();
    }
}