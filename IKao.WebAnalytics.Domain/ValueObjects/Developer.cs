namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Developer : ValueObject
{
    public uint Id { get; private set; }
    public string Name { get; private set; }

    public Developer(uint id, string name)
    {
        if (IsLengthValid(name) == false)
            throw new ArgumentException(nameof(IsLengthValid));
        
        Id = id;
        Name = name;
    }

    private bool IsLengthValid(string text)
    {
        return text.Length < 255;
    }
}