namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Description : ValueObject
{
    public string Value { get; private set; }

    protected Description() { }
    
    public Description(string value)
    {
        if (IsLengthValid(value) == false)
            throw new ArgumentException(nameof(IsLengthValid));
        
        Value = value;
    }

    private bool IsLengthValid(string text)
    {
        return text.Length < 255;
    }
}