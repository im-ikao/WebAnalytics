namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Rating : ValueObject
{
    public int Value { get; private set; }
    public int Count { get; private set; }

    protected Rating() { }
    
    public Rating(int value, int count)
    {
        if (IsInRange(value, count) == false)
            throw new ArgumentException(nameof(IsInRange));
        
        Value = value;
        Count = count;
    }

    private bool IsInRange(int value, int count)
    {
        return value is <= 5 and >= 0;
    }
}