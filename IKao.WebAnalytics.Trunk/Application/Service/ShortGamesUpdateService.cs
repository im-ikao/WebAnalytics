﻿using AutoMapper;
using EFCore.BulkExtensions;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Service;

public class ShortGamesUpdateService
{
    private readonly IEFRepository _repository;
    private readonly ILogger<ShortGamesUpdateService> _logger;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public ShortGamesUpdateService(
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
        var games = _mapper.Map<Game[]>(gamesDTO);

        if (games == null)
            throw new NullReferenceException();

        using var transaction = _repository.BeginTransaction();
        
        _repository.InsertOrUpdateBulk(games, new BulkConfig()
        {
            SetOutputIdentity = true,
            PreserveInsertOrder = false,
            UseOptionLoopJoin = false,
            PropertiesToExcludeOnUpdate = new List<string>()
            {
                nameof(Game.Description),
                nameof(Game.Seo),
                nameof(Game.Instruction),
                nameof(Game.Publish),
                nameof(Game.Play),
                nameof(Game.Media),
            },
            ReplaceReadEntities = true,
            TrackingEntities = true,
            IncludeGraph = true
        });
        
        _repository.Commit();
    }

    public async Task ProcessAsync(GameDTO[] gamesDTO)
    {
        var games = _mapper.Map<Game[]>(gamesDTO);

        if (games == null)
            throw new NullReferenceException();

        await using var transaction = await _repository.BeginTransactionAsync();
        
        await _repository.InsertOrUpdateBulkAsync(games, new BulkConfig()
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