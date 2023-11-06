using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Model;

public class Game : Entity<AppId>
{
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

    public Instant? Published { get; set; } 
    public Instant? Removed { get; set; } 
    public bool IsRemoved { get; set; } 
}