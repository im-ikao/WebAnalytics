using NodaTime;

namespace IKao.WebAnalytics.Domain.Abstraction;

public interface ISoftDeleteEntity
{
    /// <summary>
    /// Deletion Date
    /// </summary>
    public Instant? DeletionDate { get; set; }

    /// <summary>
    /// Is Deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}