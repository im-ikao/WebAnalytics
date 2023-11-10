using System.Text.RegularExpressions;
using AutoMapper;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;

public class NoHtmlConverter : IValueConverter<string, string>
{
    
    public string Convert(string sourceMember, ResolutionContext context)
    {
        if (sourceMember == null)
            return "";
        
        var noHtmlSpaces = Regex.Replace(sourceMember, @"<[^>]+>|&nbsp;", "").Trim();
        var normalized = Regex.Replace(noHtmlSpaces, @"\s{2,}", " ");
        
        return normalized;
    }
}