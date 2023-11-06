using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.DTO;

public class GameDTO : Entity<AppId>
{
    public Name Title { get; }
    public Description Description { get; }
    public Description Seo { get; }
    public Description Instruction { get; }
    public Developer Developer { get; }

    public List<Language>? Languages { get; }
    public List<Marker>? Tags { get; }
    public List<Marker>? Categories { get; }

    public AgeRating Age { get; }
    public Rating Rating { get; }
    public Counter Players { get; }

    public Media Media { get; }

    public Url Play { get; }

    public Instant? Published { get; }
    public Instant Created { get; }
    public Instant? Updated { get; }
    public Instant? Deleted { get; }
    public bool IsDeleted { get; }

    public GameDTO(Name title,
        Description description,
        Description seo,
        Description instruction,
        Developer developer,
        AgeRating age,
        Rating rating,
        Counter players,
        Media media,
        Url play,
        Instant? published,
        Instant created,
        Instant? updated,
        Instant? deleted,
        bool isDeleted,
        List<Language>? languages = null,
        List<Marker>? tags = null,
        List<Marker>? categories = null)
    {
        Title = title;
        Description = description;
        Seo = seo;
        Instruction = instruction;
        Developer = developer;
        Languages = languages;
        Tags = tags;
        Categories = categories;
        Age = age;
        Rating = rating;
        Players = players;
        Media = media;
        Play = play;
        Published = published;
        Created = created;
        Updated = updated;
        Deleted = deleted;
        IsDeleted = isDeleted;
    }
}