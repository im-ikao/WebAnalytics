using IKao.WebAnalytics.Scrape.Domain.Response;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;

public class GetShortGamesResponsesNormalizer<T> : IResponseNormalizer<T>
{
    public T Normalize(T responses)
    {
        return responses;
    }
}