namespace IKao.WebAnalytics.Domain.Model;

public class Tag : Entity<int>
{
    public string Name { get; private set; } = "";
    public virtual ICollection<Game> RelationGames { get; set; }
    
    protected Tag() { }
    
    public Tag(int id, string name) : this(id)
    {
        if (IsLengthValid(name) == false)
            throw new ArgumentException(nameof(IsLengthValid));
        
        Name = name;
    }

    public Tag(int id)
    {
        Id = id;
    }
    
    private bool IsLengthValid(string text)
    {
        return text.Length < 255;
    }
}