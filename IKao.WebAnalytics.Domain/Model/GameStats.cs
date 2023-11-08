using System.Collections;
using System.Diagnostics.Metrics;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Model;

public class GameStats : BaseEntity<AppId>
{
    protected GameStats() { }
    
    public GameStats(Instant date, AppId id, Rating rating, Counter players)
    {
        CreationDate = date;
        Id = id;
        Rating = rating;
        Players = players;
    }
    
    public Rating Rating { get; set; } 
    public Counter Players { get; set; } 
    public override Instant CreationDate { get; set; }
    

    public IEnumerator GetEnumerator()
    {
        yield return CreationDate;
        yield return Id.Value;
        yield return Players.Value;
        yield return Rating.Value;
        yield return Rating.Count;
    }
}