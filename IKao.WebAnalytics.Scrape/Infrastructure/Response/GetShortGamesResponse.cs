using IKao.WebAnalytics.Domain;
using Newtonsoft.Json;

namespace IKao.WebAnalytics.Scrape.Domain.Response;

public class GetShortGamesResponse
{
    [JsonProperty("feed")]
    public Feed[] GameFeed { get; init; }

    [JsonProperty("gamesWithPromos")]
    public long GamesWithPromos { get; init; }
        
    [JsonProperty("totalGamesCount")]
    public long TotalGamesCount { get; init; }

    [JsonProperty("lastPlayedTS")]
    public long LastPlayedTs { get; init; }
    
    [JsonProperty("pageInfo")]
    public PageInfoResponse PageInfo { get; init; }
    
    public class PageInfoResponse
    {
        [JsonProperty("nextPageId")]
        public string NextPageId { get; init; }

        [JsonProperty("rtxReqId")]
        public string RtxReqId { get; init; }

        [JsonProperty("isFirstPage")]
        public bool IsFirstPage { get; init; }

        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; init; }
    }   
    
    public class Feed
    {
        [JsonProperty("items")]
        public Item[] Items { get; init; }

        [JsonProperty("type")]
        public string Type { get; init; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; init; }

        [JsonProperty("size")]
        public string Size { get; init; }

        [JsonProperty("pageNumber")]
        public long PageNumber { get; init; }

        [JsonProperty("hasMoreItems")]
        public bool HasMoreItems { get; init; }
    }

    public class Item
    {
        [JsonProperty("developer")]
        public Developer Developer { get; init; }

        [JsonProperty("categoryIDs")]
        public int[] CategoryIDs { get; init; }

        [JsonProperty("title")]
        public string Title { get; init; }

        [JsonProperty("appID")]
        public int AppId { get; init; }

        [JsonProperty("ratingCount")]
        public int RatingCount { get; init; }

        [JsonProperty("rating")]
        public float Rating { get; init; }

        [JsonProperty("media")]
        public Media Media { get; init; }

        [JsonProperty("tagIDs")]
        public int[] TagIDs { get; init; }

        [JsonProperty("playersCount")]
        public int PlayersCount { get; init; }

        [JsonProperty("features")]
        public Features Features { get; init; }

        [JsonProperty("badge")]
        public Badge Badge { get; init; }

        [JsonProperty("column")]
        public long Column { get; init; }

        [JsonProperty("row")]
        public long Row { get; init; }

        [JsonProperty("visibility_links", NullValueHandling = NullValueHandling.Ignore)]
        public Uri[] VisibilityLinks { get; init; }

        [JsonProperty("click_link", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ClickLink { get; init; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; init; }
    }

    public class Badge
    {
    }

    public class Developer
    {
        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("id")]
        public int Id { get; init; }
    }

    public class Features
    {
        [JsonProperty("age_rating")]
        public AgeRating AgeRating { get; init; }
        
    }

    public class Media
    {
        [JsonProperty("cover")]
        public Cover Cover { get; init; }

        [JsonProperty("icon")]
        public Cover Icon { get; init; }

        [JsonProperty("videos")]
        public Video[] Videos { get; init; }
    }

    public class Cover
    {
        [JsonProperty("prefix-url")]
        public Uri PrefixUrl { get; init; }

        [JsonProperty("mainColor")]
        public string MainColor { get; init; }
    }

    public class Video
    {
        [JsonProperty("embedUrl")]
        public Uri EmbedUrl { get; init; }

        [JsonProperty("thumbnailUrl")]
        public Uri ThumbnailUrl { get; init; }

        [JsonProperty("thumbnailUrlPrefix")]
        public Uri ThumbnailUrlPrefix { get; init; }

        [JsonProperty("streamUrl")]
        public Uri StreamUrl { get; init; }

        [JsonProperty("previewUrl")]
        public Uri PreviewUrl { get; init; }

        [JsonProperty("mp4StreamUrl")]
        public Uri Mp4StreamUrl { get; init; }

        [JsonProperty("height")]
        public long Height { get; init; }

        [JsonProperty("width")]
        public long Width { get; init; }
    }
}