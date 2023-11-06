namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Language : ValueObject
{
    public string Value { get; private set; }
    
    public Language(string value)
    {
        Value = value;
    }
}