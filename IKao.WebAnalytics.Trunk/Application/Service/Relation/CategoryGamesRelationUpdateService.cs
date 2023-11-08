using AutoMapper;
using EFCore.BulkExtensions;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Domain.ValueObjects;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Service.Relation;

public class CategoryGamesRelationUpdateService
{
    private readonly IEFRepository _repository;
    private readonly ILogger<CategoryGamesRelationUpdateService> _logger;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public CategoryGamesRelationUpdateService(
        IEFRepository repository,
        ILogger<CategoryGamesRelationUpdateService> logger,
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
        var ids = _mapper.Map<AppId[]>(gamesDTO);
        var relations = _mapper.Map<GameCategoryRelation[]>(gamesDTO);

        using var transaction = _repository.BeginTransaction();
        
        // Remove where
        
        _repository.InsertOrUpdateBulk(relations, new BulkConfig()
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
        var ids = _mapper.Map<AppId[]>(gamesDTO);

        var relations = _mapper.Map<GameCategoryRelation[]>(gamesDTO);
        
        await using var transaction = await _repository.BeginTransactionAsync();
        
        // Remove where
        
        await _repository.InsertOrUpdateBulkAsync(relations, new BulkConfig()
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