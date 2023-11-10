using IKao.WebAnalytics.Scrape.Domain.Response;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;

public class GetShortGamesResponsesNormalizer : IResponseNormalizer<List<GetShortGamesResponse>>
{
    public List<GetShortGamesResponse> Normalize(List<GetShortGamesResponse> responses)
    {
        responses.ForEach(x =>
        {
            
        });
        
        return responses;
    }
}