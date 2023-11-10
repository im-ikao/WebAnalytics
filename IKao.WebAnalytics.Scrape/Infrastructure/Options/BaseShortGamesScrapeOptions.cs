namespace IKao.WebAnalytics.Scrape.Infrastructure.Options;

public class BaseShortGamesScrapeOptions
{
    public string[] Tabs = new []
    {
        "new",
        "editors_choice",
        "arcade",
        "action",
        "quiz",
        "puzzles",
        "race",
        "for_girls",
        "applications",
        "for_babies",
        "for_boys",
        "for_two_persons",
        "games_io",
        "casino",
        "casual",
        "cards",
        "imitations",
        "midcore",
        "tabletop",
        "novels",
        "educational",
        "adventure",
        "simulator",
        "sports",
        "strategy",
        "tests",
        "match3",
        "horrors",
        "balloons",
        "economic",
        "humor"
    };
    
    public string[] Languages = new []
    {
        "ru",
        "en"
    };
}