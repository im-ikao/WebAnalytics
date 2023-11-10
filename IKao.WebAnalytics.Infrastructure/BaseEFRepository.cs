using System.Linq.Expressions;
using IKao.WebAnalytics.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace IKao.WebAnalytics.Infrastructure;

public class BaseEFRepository : IEFRepository
{
    private readonly DbContext _context;
    
    public BaseEFRepository(DbContext context)
    {
        this._context = context;
    }
    
    public TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Set<TEntity>().Add(entity);
        return entity;
    }

    public TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey>
    {
        entity.CreationDate = SystemClock.Instance.GetCurrentInstant();
        entity.ModificationDate = SystemClock.Instance.GetCurrentInstant();
        
        _context.Set<TEntity>().Add(entity);
        return entity;
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        return entity;
    }

    public async Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey>
    {
        entity.CreationDate = SystemClock.Instance.GetCurrentInstant();
        entity.ModificationDate = SystemClock.Instance.GetCurrentInstant();
        
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        return entity;
    }

    public void InsertOrUpdateBulk<TEntity>(TEntity[] entity, BulkConfig config) where TEntity : class
    {
        _context.BulkInsertOrUpdate(entity, config);
    }

    public void InsertOrUpdateBulk<TEntity, TPrimaryKey>(TEntity[] entity, BulkConfig config) where TEntity : BaseEntity<TPrimaryKey>
    {
        InsertBaseEntityBulkConfigBootstrap<TEntity, TPrimaryKey>(config);
        _context.BulkInsertOrUpdate(entity, config);
    }

    public async Task InsertOrUpdateBulkAsync<TEntity>(TEntity[] entity, BulkConfig config, CancellationToken cancellationToken = default) where TEntity : class
    {
        await _context.BulkInsertOrUpdateAsync(entity, config, cancellationToken: cancellationToken);
    }

    public async Task InsertOrUpdateBulkAsync<TEntity, TPrimaryKey>(TEntity[] entity, BulkConfig config, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey>
    {
        InsertBaseEntityBulkConfigBootstrap<TEntity, TPrimaryKey>(config);
        await _context.BulkInsertOrUpdateAsync(entity, config, cancellationToken: cancellationToken);
    }

    public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    public IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
    {
        return _context.Set<TEntity>().Where(filter);
    }
    
    private IQueryable<TEntity> FindQueryable<TEntity>(bool asNoTracking) where TEntity : class
    {
        var queryable = GetQueryable<TEntity>();
        if (asNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }
        return queryable;
    }

    public List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class
    {
        return FindQueryable<TEntity>(asNoTracking).ToList();
    }

    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class
    {
        return await FindQueryable<TEntity>(asNoTracking).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class
    {
        return await FindQueryable<TEntity>(asNoTracking).Where(whereExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public void Commit()
    {
        _context.Database.CommitTransaction();
    }

    public Task CommitAsync()
    {
        return _context.Database.CommitTransactionAsync();
    }

    private static void InsertBaseEntityBulkConfigBootstrap<TEntity, TPrimaryKey>(BulkConfig config)
        where TEntity : BaseEntity<TPrimaryKey>
    {
        if (config.PropertiesToExcludeOnUpdate == null)
            config.PropertiesToExcludeOnUpdate = new List<string>();

        config.PropertiesToExcludeOnUpdate.Add(nameof(BaseEntity<TPrimaryKey>.CreationDate));
        config.PropertiesToExcludeOnUpdate.Add(nameof(BaseEntity<TPrimaryKey>.DeletionDate));
    }
}