namespace IKao.WebAnalytics.Domain.ValueObjects;

public class AppId : ValueObject
{
    public uint Value { get; private set; }

    protected AppId() { }

    public AppId(uint value)
    {
        Value = value;
    }
}