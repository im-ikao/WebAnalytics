using AutoMapper;
using EFCore.BulkExtensions;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Service;

public class GameStatsUpdateService
{
    private readonly IEFRepository _repository;
    private readonly ILogger<ShortGamesUpdateService> _logger;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public GameStatsUpdateService(
        IEFRepository repository,
        ILogger<ShortGamesUpdateService> logger,
        IMapper mapper,
        IBus bus)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
    }

    public void Process(GameDTO[] gamesDTO)
    {
        var games = _mapper.Map<GameStats[]>(gamesDTO);

        if (games == null)
            throw new NullReferenceException();

        using var transaction = _repository.BeginTransaction();
        
        // TODO: CLICKHOUSE 
        
        _repository.Commit();
    }

    public async Task ProcessAsync(GameDTO[] gamesDTO)
    {
        var games = _mapper.Map<GameStats[]>(gamesDTO);

        if (games == null)
            throw new NullReferenceException();

        await using var transaction = await _repository.BeginTransactionAsync();
        
        // TODO: CLICKHOUSE 
        
        await _repository.CommitAsync();
    }
}