namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Counter : ValueObject
{
    public uint Value { get; private set; }

    public Counter(uint value)
    {
        Value = value;
    }
}