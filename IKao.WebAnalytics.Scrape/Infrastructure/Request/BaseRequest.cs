using Cysharp.Web;
using IKao.WebAnalytics.Scrape.Domain.Response;
using Newtonsoft.Json;
using RestSharp;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Request;

public class BaseRequest<TResponse> : IRequest<TResponse>
{
    protected readonly string _basePath;

    protected BaseRequest(string basePath)
    {
        _basePath = basePath;
    }

    public async Task<TResponse?> ExecuteAsync(IRestClient client, CancellationToken token)
    {
        var request = BootstrapRequest(null);
        var response = await ExecuteAsync(client, request, token);
        
        if (response == null)
            throw new HttpRequestException();

        if (response.IsSuccessful == false)
            throw new HttpRequestException();

        if (response.Content == null)
            throw new AggregateException();
            
        return JsonConvert.DeserializeObject<TResponse>(response.Content);
    }

    protected virtual RestRequest BootstrapRequest(RestRequest? request)
    {
        return new RestRequest($"{_basePath}?{WebSerializer.ToQueryString(this)}");
    }

    protected virtual Task<RestResponse> ExecuteAsync(IRestClient client, RestRequest request, CancellationToken token)
    {
        return client.GetAsync(request, token);
    }
}