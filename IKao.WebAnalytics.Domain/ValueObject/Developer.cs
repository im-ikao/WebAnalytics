namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Developer : ValueObject
{
    public int Id { get; private set; }
    public string Name { get; private set; } = "";

    protected Developer() { }
    
    public Developer(int id, string name) : this(id)
    {
        if (IsLengthValid(name) == false)
            throw new ArgumentException(nameof(IsLengthValid));
        
        Name = name;
    }

    public Developer(int id)
    {
        Id = id;
    }
    
    private bool IsLengthValid(string text)
    {
        return text.Length < 255;
    }
}