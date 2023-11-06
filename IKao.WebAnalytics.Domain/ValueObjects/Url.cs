namespace IKao.WebAnalytics.Domain.ValueObjects;

public class Url : ValueObject
{
    public string Value { get; private set; }

    protected Url() { }
    
    public Url(string value)
    {
        if (IsUrl(value) == false)
            throw new ArgumentException(nameof(IsUrl));
        
        Value = value;
    }

    private bool IsUrl(string text)
    {
        return Uri.TryCreate(text, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}