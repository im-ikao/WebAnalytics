using AutoMapper;
using ComposableAsync;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.RateLimit;
using IKao.WebAnalytics.Scrape.Domain.Response;
using IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;
using IKao.WebAnalytics.Scrape.Infrastructure.Options;
using IKao.WebAnalytics.Scrape.Infrastructure.Request;
using MassTransit;
using Polly;
using RestSharp;

namespace IKao.WebAnalytics.Scrape.Infrastructure;

public class BaseShortGamesScrape : IScrape<GetShortGamesResponse>
{
    private readonly IRestClient _client;
    private readonly BaseShortGamesScrapeOptions _options;
    private readonly IResponseNormalizer<GetShortGamesResponse> _normalizer;
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly TimeLimiter _rate;

    public BaseShortGamesScrape(IRestClient client,
        BaseShortGamesScrapeOptions options,
        IResponseNormalizer<GetShortGamesResponse> normalizer,
        IBus bus,
        IMapper mapper,
        TimeLimiter rate)
    {
        _client = client;
        _options = options;
        _normalizer = normalizer;
        _bus = bus;
        _mapper = mapper;
        _rate = rate;
    }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public async Task ExecuteAsync(CancellationToken token)
    {
        await RequestsByOptionsAsync(_options.Tabs, token);
    }

    public async Task RequestsByOptionsAsync(string[] tabs, CancellationToken token)
    {
        foreach (var tab in tabs)
        {
            foreach (var lang in _options.Languages)
            {
                await RequestByOptionAsync(tab, lang, token);
            }
        }
    }

    public async Task RequestByOptionAsync(string tab, string language, CancellationToken token)
    {
        var request = new GetShortGamesRequest(64, false, null,
            tab, language, GetShortGamesRequest.DeviceType.Desktop,
            GetShortGamesRequest.PlaformType.DesktopOther, null, null);
        
        foreach (GetShortGamesRequest.DeviceType device in Enum.GetValues(typeof(GetShortGamesRequest.DeviceType)))
        {
            request.SetDevice(device);
            
            foreach (GetShortGamesRequest.PlaformType platform in Enum.GetValues(typeof(GetShortGamesRequest.PlaformType)))
            {
                request.SetPlatform(platform);
                request.Clear();
                
                await GetResponsesAsync(request, OnBatchCollected, token);
            } 
        }
    }
    
    public async Task GetResponsesAsync(GetShortGamesRequest request, IScrape<GetShortGamesResponse>.BatchCollected collected, CancellationToken token)
    {
        var games = new List<GetShortGamesResponse>();

        while (true)
        {
            var retry = Policy<GetShortGamesResponse>
                .Handle<NullReferenceException>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(5));

            await _rate;
            var response = await retry.ExecuteAsync(async () => await GetResponseAsync(request, token));

            games.Add(response);
            _normalizer.Normalize(response);
            
            collected.Invoke(games, token); // TODO: SEND AFTER BATCH 
            games.Clear();
            
            if (response.PageInfo.HasNextPage == false)
                break;
            
            request.SetPageId(response.PageInfo.NextPageId);
            request.SetRequestToken(response.PageInfo.RtxReqId);
        }
    }

    public async Task<GetShortGamesResponse> GetResponseAsync(GetShortGamesRequest request, CancellationToken token)
    {
        var response = await request.ExecuteAsync(_client, token);

        if (response == null)
            throw new NullReferenceException();
        
        return response;
    }

    private async Task OnBatchCollected(List<GetShortGamesResponse> responses, CancellationToken token)
    {
        var games = _mapper.Map<GameDTO[]>(responses);
        
        await _bus.Publish<IShortGamesUpdateRequestMessage>(new
        {
            Games = games
        }, token);
    } 

}