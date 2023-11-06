namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Developer : ValueObject
{
    public uint Id { get; private set; }
    public string Name { get; private set; } = "";

    protected Developer() { }
    
    public Developer(uint id, string name) : this(id)
    {
        if (IsLengthValid(name) == false)
            throw new ArgumentException(nameof(IsLengthValid));
        
        Name = name;
    }

    public Developer(uint id)
    {
        Id = id;
    }
    
    private bool IsLengthValid(string text)
    {
        return text.Length < 255;
    }
}