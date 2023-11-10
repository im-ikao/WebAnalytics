using AutoMapper;
using ComposableAsync;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Extension;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using IKao.WebAnalytics.RateLimit;
using IKao.WebAnalytics.Scrape.Domain.Request;
using IKao.WebAnalytics.Scrape.Domain.Response;
using IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;
using IKao.WebAnalytics.Scrape.Infrastructure.Options;
using MassTransit;
using NodaTime;
using Polly;
using RestSharp;

namespace IKao.WebAnalytics.Scrape.Infrastructure;

public class BaseLongGameScrape : IScrape<GetLongGamesResponse>
{
    private readonly IRestClient _client;
    private readonly IResponseNormalizer<GetLongGamesResponse> _normalizer;
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly TimeLimiter _rate;
    private readonly IEFRepository _repository;

    public BaseLongGameScrape(
        IRestClient client,
        IResponseNormalizer<GetLongGamesResponse> normalizer,
        IBus bus,
        IMapper mapper,
        TimeLimiter rate,
        IEFRepository repository)
    {
        _client = client;
        _normalizer = normalizer;
        _bus = bus;
        _mapper = mapper;
        _rate = rate;
        _repository = repository;
    }
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public async Task ExecuteAsync(CancellationToken token)
    {
        var responses = new List<GetLongGamesResponse>();
        var request = new GetLongGamesRequest(Array.Empty<int>(), "long");
        var retrieved = new PagedList<AppId>(Array.Empty<AppId>(), 0, 0, 0);
        var isFirstRequest = true;
        
        do 
        {
            retrieved = await _repository
                .GetQueryable<Game>()
                .OrderBy(x => x.Id)
                .Where(x => x.ModificationDate.Plus(Duration.FromHours(4)) < SystemClock.Instance.GetCurrentInstant())
                .Select(x => x.Id)
                .ToPagedListAsync(isFirstRequest ? retrieved.PageNumber : retrieved.PageNumber + 1, 100, token);
            
            isFirstRequest = false;
            
            var ids = retrieved
                .Select(x => x.Value)
                .ToArray();

            request.AppIDs = ids;
            
            var retry = Policy<GetLongGamesResponse>
                .Handle<NullReferenceException>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(5));

            await _rate;
            var response = await retry.ExecuteAsync(async () => await request.ExecuteAsync(_client, token) ?? throw new NullReferenceException());
            
            responses.Add(response);
            _normalizer.Normalize(response);
            
            await OnBatchCollected(responses, token);
            responses.Clear();
            
            await _bus.Publish<ILongGamesUpdateRequestMessage>(new
            {
                Games = responses
            }, token);
            
        } while (retrieved.IsLastPage);
    }

    private async Task OnBatchCollected(List<GetLongGamesResponse> responses, CancellationToken token)
    {
        var games = _mapper.Map<GameDTO[]>(responses);
        
        await _bus.Publish<IShortGamesUpdateRequestMessage>(new
        {
            Games = games
        }, token);
    } 
}