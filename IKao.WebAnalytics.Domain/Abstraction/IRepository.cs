namespace IKao.WebAnalytics.Domain.Abstraction;

public interface IRepository
{
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert or update operation. In additional this methods returns <see cref="{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <returns>
    /// Returns <see cref="{TEntity}"/>
    /// </returns>
    TEntity Add<TEntity>(TEntity entity) where TEntity : class;
    
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert or update operation. In additional this methods returns <see cref="{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <returns>
    /// Returns <see cref="{TEntity}"/>
    /// </returns>
    TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey>;

    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert async. In additional this methods returns <see cref="Task{TEntity}"/> 
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task{TEntity}"/>
    /// </returns>
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert async. In additional this methods returns <see cref="Task{TEntity}"/> 
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task{TEntity}"/>
    /// </returns>
    Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey>;
}