namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; private set; }

    protected Name() { }
    
    public Name(string value)
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