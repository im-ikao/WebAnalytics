namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Rating : ValueObject
{
    public float Value { get; private set; }
    public int Count { get; private set; }

    protected Rating() { }
    
    public Rating(float value, int count)
    {
        if (IsInRange(value, count) == false)
            throw new ArgumentException(nameof(IsInRange));
        
        Value = value;
        Count = count;
    }

    private bool IsInRange(float value, int count)
    {
        return value is <= 5 and >= 0;
    }
}