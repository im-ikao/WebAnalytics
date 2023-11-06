namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Marker : ValueObject
{
    public int Id { get; private set; }
    
    public Marker(int id)
    {
        Id = id;
    }
    
}