using NodaTime;

namespace IKao.WebAnalytics.Domain.Abstraction;

public class BaseEntity<TPrimaryKey>  : Entity<TPrimaryKey>, ICreateDateEntity, IUpdateDateEntity, ISoftDeleteEntity
{
    /// <summary>
    /// Creation Date <see>
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary>
    public virtual Instant CreationDate { get; set; }

    /// <summary>
    /// Modification Date <see>
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary>
    public virtual Instant? ModificationDate { get; set; }

    /// <summary>
    /// Deletion Date <see>
    ///     <cref>{DateTime}</cref>
    /// </see>
    /// </summary>
    public virtual Instant? DeletionDate { get; set; }

    /// <summary>
    /// Is Deleted <see>
    ///     <cref>{Boolean}</cref>
    /// </see>
    /// </summary>
    public virtual bool IsDeleted { get; set; }
}