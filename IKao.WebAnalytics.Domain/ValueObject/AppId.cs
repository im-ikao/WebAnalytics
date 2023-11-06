namespace IKao.WebAnalytics.Domain.ValueObjects;

public class AppId : ValueObject
{
    public int Value { get; private set; }

    protected AppId() { }

    public AppId(int value)
    {
        Value = value;
    }
}