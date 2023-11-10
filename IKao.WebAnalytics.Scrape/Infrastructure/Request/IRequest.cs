using RestSharp;

namespace IKao.WebAnalytics.Scrape.Domain.Response;

public interface IRequest<TResponse>
{
    public Task<TResponse?> ExecuteAsync(IRestClient client, CancellationToken token);
}