namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Media : ValueObject
{
    public Url Cover  { get; private set; }
    public Url Icon { get; private set; }
    
    protected Media() { }

    public Media(Url cover, Url icon)
    {
        Cover = cover;
        Icon = icon;
    }
}