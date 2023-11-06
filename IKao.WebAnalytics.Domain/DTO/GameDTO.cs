using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.DTO;

public class GameDTO : Entity<AppId>
{
    private Name _title;
    private Description _description;
    private Description _seo;
    private Description _instruction;
    private Developer _developer;
    
    private List<Language> _languages;
    private List<Marker> _tags;
    private List<Marker> _categories;

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