using NodaTime;

namespace IKao.WebAnalytics.Domain.Abstraction;

public interface IUpdateDateEntity
{
    /// <summary>
    /// Modification Date
    /// </summary>
    public Instant ModificationDate { get; set; }
}