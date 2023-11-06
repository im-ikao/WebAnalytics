namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Media : ValueObject
{
    public Url Cover  { get; private set; }
    public Url Icon { get; private set; }
    public Url[] Videos { get; private set; }
    public Url[] Screenshots { get; private set; }

    public Media(Url cover, Url icon, Url[] videos, Url[] screenshots)
    {
        Cover = cover;
        Icon = icon;
        Videos = videos;
        Screenshots = screenshots;
    }
}