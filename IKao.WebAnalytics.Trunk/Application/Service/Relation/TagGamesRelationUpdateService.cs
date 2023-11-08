using AutoMapper;
using EFCore.BulkExtensions;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Domain.ValueObjects;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Service.Relation;

public class TagGamesRelationUpdateService
{
    private readonly IEFRepository _repository;
    private readonly ILogger<TagGamesRelationUpdateService> _logger;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public TagGamesRelationUpdateService(
        IEFRepository repository,
        ILogger<TagGamesRelationUpdateService> logger,
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
        var ids = gamesDTO
            .Select(x => x.Id)
            .ToArray();

        var tags = gamesDTO
            .SelectMany(x => x.Tags ?? new List<Marker>())
            .Select(x => _mapper.Map<GameTagRelation>(x))
            .ToArray();

        using var transaction = _repository.BeginTransaction();
        
        // Remove where
        
        _repository.InsertOrUpdateBulk(tags, new BulkConfig()
        {
            SetOutputIdentity = true,
            PreserveInsertOrder = false,
            UseOptionLoopJoin = false,
            ReplaceReadEntities = true,
            TrackingEntities = true,
            IncludeGraph = true
        });
        
        _repository.Commit();
    }

    public async Task ProcessAsync(GameDTO[] gamesDTO)
    {
        var ids = gamesDTO
            .Select(x => x.Id)
            .ToArray();

        var tags = gamesDTO
            .SelectMany(x => x.Tags ?? new List<Marker>())
            .Select(x => _mapper.Map<GameTagRelation>(x))
            .ToArray();
        
        await using var transaction = await _repository.BeginTransactionAsync();
        
        // Remove where
        
        await _repository.InsertOrUpdateBulkAsync(tags, new BulkConfig()
        {
            SetOutputIdentity = true,
            PreserveInsertOrder = false,
            UseOptionLoopJoin = false,
            ReplaceReadEntities = true,
            TrackingEntities = true,
            IncludeGraph = true
        });
        
        await _repository.CommitAsync();
    }
}