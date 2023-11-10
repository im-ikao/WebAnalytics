using IKao.WebAnalytics.Scrape.Domain.Response;
using IKao.WebAnalytics.Scrape.Infrastructure.Request;
using Newtonsoft.Json;
using RestSharp;

namespace IKao.WebAnalytics.Scrape.Domain.Request;

public class GetLongGamesRequest : BaseRequest<GetLongGamesResponse>
{
    [JsonProperty("appIDs")]
    public int[] AppIDs { get; set; }

    [JsonProperty("format")]
    public string Format { get; set; }

    public GetLongGamesRequest(int[] ids, string format) : base("catalogue/v2/get_games")
    {
        AppIDs = ids;
        Format = format;
    }

    protected override RestRequest BootstrapRequest(RestRequest? request)
    {
        request = new RestRequest($"{_basePath}");
        
        var query = JsonConvert.SerializeObject(this);
        
        request.AddStringBody(query, DataFormat.Json);

        request.Method = Method.Post;

        return request;
    }

    protected override Task<RestResponse> ExecuteAsync(IRestClient client, RestRequest request, CancellationToken token)
    {
        return client.PostAsync(request, token);
    }
}