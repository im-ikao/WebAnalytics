namespace IKao.WebAnalytics.Domain.Model;

public class Language : Entity<int>
{
    public string Value { get; private set; }
    public virtual ICollection<Game> RelationGames { get; set; }
    
    protected Language() { }
    
    public Language(string value)
    {
        if (IsLengthValid(value) == false)
            throw new ArgumentException();
        
        Value = value;
    }
    
    public Language(int id, string value) : this(value)
    {
        Id = id;
    }
    
    private bool IsLengthValid(string text)
    {
        return text.Length <= 6;
    }
}