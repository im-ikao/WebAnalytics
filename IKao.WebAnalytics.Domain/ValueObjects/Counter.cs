namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Counter : ValueObject
{
    public uint Value { get; private set; }

    protected Counter() { }
    
    public Counter(uint value)
    {
        Value = value;
    }
}