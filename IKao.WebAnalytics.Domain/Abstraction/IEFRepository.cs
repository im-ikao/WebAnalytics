using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace IKao.WebAnalytics.Domain.Abstraction;

public interface IEFRepository : IRepository
{
    public void InsertOrUpdateBulk<TEntity>(TEntity[] entity, BulkConfig config) where TEntity : class;
    public void InsertOrUpdateBulk<TEntity, TPrimaryKey>(TEntity[] entity, BulkConfig config) where TEntity : BaseEntity<TPrimaryKey>;
    public Task InsertOrUpdateBulkAsync<TEntity>(TEntity[] entity, BulkConfig config, CancellationToken cancellationToken = default) where TEntity : class;
    public Task InsertOrUpdateBulkAsync<TEntity, TPrimaryKey>(TEntity[] entity, BulkConfig config, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey>;
    IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
    IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
    List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class;
    Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class;
    Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class;

    
    public IDbContextTransaction BeginTransaction();
    public Task<IDbContextTransaction> BeginTransactionAsync();
    public void Commit();
    public Task CommitAsync();
}