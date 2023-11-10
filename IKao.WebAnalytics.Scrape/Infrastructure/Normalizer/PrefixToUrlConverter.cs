using AutoMapper;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;

public class PrefixToUrlConverter : IValueConverter<string, string>
{
    private readonly int _width;
    private readonly int _height;
    
    public PrefixToUrlConverter(int width, int height)
    {
        _width = width;
        _height = height;
    }
    
    public string Convert(string sourceMember, ResolutionContext context)
    {
        return $"{sourceMember}pjpg{_width}x{_height}";
    }
}