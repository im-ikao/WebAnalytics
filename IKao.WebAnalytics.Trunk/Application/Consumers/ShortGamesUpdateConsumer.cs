using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Trunk.Application.Service;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Consumers;

public class ShortGamesUpdateConsumer : IConsumer<IShortGamesUpdateRequestMessage>
{
    private readonly ILogger<IShortGamesUpdateRequestMessage> _logger;
    private readonly ShortGamesUpdateService _games;
    private readonly DeveloperUpdateService _developers;
    private readonly GameStatsUpdateService _stats;

    public ShortGamesUpdateConsumer(ILogger<IShortGamesUpdateRequestMessage> logger,
        ShortGamesUpdateService games,
        DeveloperUpdateService developers,
        GameStatsUpdateService stats)
    {
        _logger = logger;
        _games = games;
        _developers = developers;
        _stats = stats;
    }
    
    public async Task Consume(ConsumeContext<IShortGamesUpdateRequestMessage> context)
    {
        await _developers.ProcessAsync(context.Message.Games);
        await _games.ProcessAsync(context.Message.Games);
        await _stats.ProcessAsync(context.Message.Games);
    }
}