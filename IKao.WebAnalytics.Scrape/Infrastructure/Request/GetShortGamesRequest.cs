using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AutoMapper;
using Cysharp.Web;
using IKao.WebAnalytics.Scrape.Domain.Response;
using Newtonsoft.Json;
using RestSharp;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Request;

public class GetShortGamesRequest : BaseRequest<GetShortGamesResponse>
{

    public GetShortGamesRequest(
        int count,
        bool hasPromos,
        string? publisher,
        string tab,
        string language,
        DeviceType device,
        PlaformType platform,
        string? pageId,
        string? requestToken) : base("catalogue/v2/feed")
    {
        Count = count;
        HasPromos = hasPromos;
        Publisher = publisher;
        Tab = tab;
        Language = language;
        Device = device;
        Platform = platform;
        PageId = pageId;
        RequestToken = requestToken;
    }
    
    [JsonProperty("games_count")]
    [DataMember(Name = "games_count", Order = 0)]
    public int Count;
    
    [JsonProperty("with_promos")]
    [DataMember(Name = "with_promos", Order = 1)]
    public bool HasPromos;

    [JsonProperty("publisher")]
    [DataMember(Name = "publisher", Order = 7)]
    public string? Publisher;
    
    [JsonProperty("tab")]
    [DataMember(Name = "tab", Order = 2)]
    public string Tab;
    
    [JsonProperty("lang")]
    [DataMember(Name = "lang", Order = 3)]
    public string Language;
    
    [JsonProperty("device-type")]
    [DataMember(Name = "device-type", Order = 4)]
    public DeviceType Device;

    [JsonProperty("platform")]
    [DataMember(Name = "platform", Order = 4)]
    public PlaformType Platform;
    
    [JsonProperty("page_id")]
    [DataMember(Name = "page_id", Order = 6)]
    public string? PageId;
    
    [JsonProperty("rtx-reqid")]
    [DataMember(Name = "rtx-reqid", Order = 7)]
    public string? RequestToken;
    
    public enum DeviceType
    {
        [Display(Name = "desktop")]
        [EnumMember(Value = "desktop")]
        Desktop,
        [Display(Name = "mobile")]
        [EnumMember(Value = "mobile")]
        Mobile
    }
    public enum PlaformType
    {
        [Display(Name = "desktop_other")]
        [EnumMember(Value = "desktop_other")]
        DesktopOther,
        [Display(Name = "mobile")]
        [EnumMember(Value = "mobile")]
        Mobile
    }

    public void SetPageId(string pageId)
    {
        PageId = pageId;
    }

    public void SetRequestToken(string requestToken)
    {
        RequestToken = requestToken;
    }

    public void SetDevice(DeviceType device)
    {
        Device = device;
    }

    public void SetPlatform(PlaformType platform)
    {
        Platform = platform;
    }

    public void Clear()
    {
        PageId = null;
        RequestToken = null;
    }
}