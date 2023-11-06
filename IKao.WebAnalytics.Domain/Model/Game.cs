using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Model;

public class Game : Entity<AppId>
{
    private Name _title;
    private Description _description;
    private Description _seo;
    private Description _instruction;
    private Developer _developer;

    private AgeRating _age;
    private Rating _rating;
    private Counter _players;

    private Media _media;
    
    private Url _play;

    private Instant? _published;
    private Instant? _created;
    private Instant? _updated;
    private Instant? _removed;
}