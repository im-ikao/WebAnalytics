using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Model;

public sealed class Game : BaseEntity<AppId>
{
    protected Game() { }
    
    public Game(Name title, Description description, Description seo, Description instruction, Developer developer, AgeRating age, Rating rating, Counter players, Media media, Url play, Instant? publish, Instant creationDate, Instant modificationDate, Instant? deletionDate, bool isDeleted)
    {
        Title = title;
        Description = description;
        Seo = seo;
        Instruction = instruction;
        Developer = developer;
        Age = age;
        Rating = rating;
        Players = players;
        Media = media;
        Play = play;
        Publish = publish;
        CreationDate = creationDate;
        ModificationDate = modificationDate;
        DeletionDate = deletionDate;
        IsDeleted = isDeleted;
    }

    public Name Title { get; set; }
    public Description Description { get; set; } 
    public Description Seo { get; set; } 
    public Description Instruction { get; set; } 
    public Developer Developer { get; set; } 

    public AgeRating Age { get; set; } 
    public Rating Rating { get; set; } 
    public Counter Players { get; set; } 

    public Media Media { get; set; } 
    
    public Url Play { get; set; } 

    public Instant? Publish { get; set; } 
    public override Instant CreationDate { get; set; }
    public override Instant ModificationDate { get; set; }
    public override Instant? DeletionDate { get; set; }
    public override bool IsDeleted { get; set; }
    
    public List<Language> RelationLanguages { get; } = new();
    public List<Category> RelationCategories { get; } = new();
    public List<Tag> RelationTags { get; } = new();

}