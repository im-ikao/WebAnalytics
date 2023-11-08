using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Trunk.Application.Service;
using IKao.WebAnalytics.Trunk.Application.Service.Relation;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Consumers;

public class LongGamesUpdateConsumer : IConsumer<ILongGamesUpdateRequestMessage>
{
    private readonly ILogger<ILongGamesUpdateRequestMessage> _logger;
    private readonly LongGamesUpdateService _games;
    private readonly CategoryGamesRelationUpdateService _categories;
    private readonly TagGamesRelationUpdateService _tags;
    private readonly LanguageGamesRelationUpdateService _languages;
    private readonly DeveloperUpdateService _developers;
    private readonly GameStatsUpdateService _stats;

    public LongGamesUpdateConsumer(ILogger<ILongGamesUpdateRequestMessage> logger,
        LongGamesUpdateService games,
        CategoryGamesRelationUpdateService categories,
        TagGamesRelationUpdateService tags,
        LanguageGamesRelationUpdateService languages,
        DeveloperUpdateService developers,
        GameStatsUpdateService stats)
    {
        _logger = logger;
        _games = games;
        _categories = categories;
        _tags = tags;
        _languages = languages;
        _developers = developers;
        _stats = stats;
    }
    
    public async Task Consume(ConsumeContext<ILongGamesUpdateRequestMessage> context)
    {
        await _developers.ProcessAsync(context.Message.Games);
        await _games.ProcessAsync(context.Message.Games);
        await _tags.ProcessAsync(context.Message.Games);
        await _languages.ProcessAsync(context.Message.Games);
        await _categories.ProcessAsync(context.Message.Games);
        await _stats.ProcessAsync(context.Message.Games);
    }
}