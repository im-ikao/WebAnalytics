using IKao.WebAnalytics.Domain;
using Newtonsoft.Json;

namespace IKao.WebAnalytics.Scrape.Domain.Response;

public class GetLongGamesResponse
{
    [JsonProperty("games")]
    public Game[] Games { get; set; }
    
    public class Game
    {
        [JsonProperty("categoryIDs")]
        public int[] CategoryIDs { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("appID")]
        public int AppId { get; set; }

        [JsonProperty("ratingCount")]
        public int RatingCount { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("categoriesNames")]
        public string[] CategoriesNames { get; set; }
        
        [JsonProperty("tagIDs")]
        public int[] TagIDs { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("score")]
        public Dictionary<string, long> Score { get; set; }

        [JsonProperty("seoDescription")]
        public string SeoDescription { get; set; }

        [JsonProperty("instruction")]
        public string Instruction { get; set; }

        [JsonProperty("premiumDescription")]
        public string PremiumDescription { get; set; }

        [JsonProperty("playersCount")]
        public int PlayersCount { get; set; }

        [JsonProperty("firstPublished")]
        public long FirstPublished { get; set; }
        
        [JsonProperty("media")]
        public GetShortGamesResponse.Media Media { get; set; }
        
        [JsonProperty("features")]
        public GameFeatures Features { get; set; }
        
        [JsonProperty("developer")]
        public GetShortGamesResponse.Developer Developer { get; set; }
    }
    
    public class GameFeatures
    {
        [JsonProperty("age_rating")]
        public AgeRating AgeRating { get; set; }
        
        [JsonProperty("languages")]
        public string[] Languages { get; set; }
    }
}