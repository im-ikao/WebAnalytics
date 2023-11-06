namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Counter : ValueObject
{
    public int Value { get; private set; }

    protected Counter() { }
    
    public Counter(int value)
    {
        Value = value;
    }
}