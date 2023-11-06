using NodaTime;

namespace IKao.WebAnalytics.Domain.Abstraction;

public interface ICreateDateEntity
{
    /// <summary>
    /// Creation Date
    /// </summary>
    public Instant CreationDate { get; set; }
}