namespace IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;

public interface IResponseNormalizer<TResponse>
{
    public TResponse Normalize(TResponse response);
}