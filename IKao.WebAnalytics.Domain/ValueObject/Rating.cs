namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Rating : ValueObject
{
    public uint Value { get; private set; }
    public uint Count { get; private set; }

    protected Rating() { }
    
    public Rating(uint value, uint count)
    {
        if (IsInRange(value, count) == false)
            throw new ArgumentException(nameof(IsInRange));
        
        Value = value;
        Count = count;
    }

    private bool IsInRange(uint value, uint count)
    {
        return value is <= 5 and >= 0;
    }
}