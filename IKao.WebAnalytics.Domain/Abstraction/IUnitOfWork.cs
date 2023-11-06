namespace IKao.WebAnalytics.Domain.Abstraction;

/// <summary>
/// Abstraction of Unit Of Work pattern
/// </summary>
public interface IUnitOfWork
{
    IRepository Repository { get; }
}