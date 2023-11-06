namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Marker : ValueObject
{
    public uint Id { get; private set; }

    public Marker(uint id)
    {
        Id = id;
    }
    
}