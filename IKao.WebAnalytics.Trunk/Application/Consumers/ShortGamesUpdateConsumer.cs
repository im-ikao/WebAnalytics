using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Trunk.Application.Service;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Consumers;

public class ShortGamesUpdateConsumer : IConsumer<IShortGamesUpdateRequestMessage>
{
    private readonly ILogger<IShortGamesUpdateRequestMessage> _logger;
    private readonly ShortGamesUpdateService _games;
    private readonly DeveloperUpdateService _developers;
    
    public ShortGamesUpdateConsumer(ILogger<IShortGamesUpdateRequestMessage> logger, ShortGamesUpdateService games, DeveloperUpdateService developers)
    {
        _logger = logger;
        _games = games;
        _developers = developers;
    }
    
    public async Task Consume(ConsumeContext<IShortGamesUpdateRequestMessage> context)
    {
        await _developers.ProcessAsync(context.Message.Games);
        await _games.ProcessAsync(context.Message.Games);
        // TODO: UPDATE RECORD IN TIME;
    }
}