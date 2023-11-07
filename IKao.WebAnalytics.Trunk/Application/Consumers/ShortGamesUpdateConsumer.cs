using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Trunk.Application.Service;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Consumers;

public class ShortGamesUpdateConsumer : IConsumer<IShortGamesUpdateRequestMessage>
{
    private readonly ILogger<IShortGamesUpdateRequestMessage> _logger;
    private readonly ShortGamesUpdateService _gamesUpdateService;

    public ShortGamesUpdateConsumer(ILogger<IShortGamesUpdateRequestMessage> logger, ShortGamesUpdateService gamesUpdateService)
    {
        _logger = logger;
        _gamesUpdateService = gamesUpdateService;
    }
    
    public async Task Consume(ConsumeContext<IShortGamesUpdateRequestMessage> context)
    {
        await _gamesUpdateService.ProcessAsync(context.Message.Games);
        // TODO: UPDATE RECORD IN TIME;
        // TODO: UPDATE DEVELOPERS ?;
    }
}