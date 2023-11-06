﻿using IKao.WebAnalytics.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using EFCore.BulkExtensions;

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

    private static void InsertBaseEntityBulkConfigBootstrap<TEntity, TPrimaryKey>(BulkConfig config)
        where TEntity : BaseEntity<TPrimaryKey>
    {
        if (config.PropertiesToExcludeOnUpdate == null)
            config.PropertiesToExcludeOnUpdate = new List<string>();

        config.PropertiesToExcludeOnUpdate.Add(nameof(BaseEntity<TPrimaryKey>.CreationDate));
        config.PropertiesToExcludeOnUpdate.Add(nameof(BaseEntity<TPrimaryKey>.DeletionDate));
    }
}