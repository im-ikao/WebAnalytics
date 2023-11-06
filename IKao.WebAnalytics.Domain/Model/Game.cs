using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Model;

public class Game : BaseEntity<AppId>
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
    public virtual Instant CreationDate { get; set; }
    public virtual Instant? ModificationDate { get; set; }
    public virtual Instant? DeletionDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}