using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Trunk.Application.Service;
using MassTransit;

namespace IKao.WebAnalytics.Trunk.Application.Consumers;

public class LongGamesUpdateConsumer : IConsumer<ILongGamesUpdateRequestMessage>
{
    private readonly ILogger<ILongGamesUpdateRequestMessage> _logger;
    private readonly LongGamesUpdateService _gamesUpdateService;

    public LongGamesUpdateConsumer(ILogger<ILongGamesUpdateRequestMessage> logger, LongGamesUpdateService gamesUpdateService)
    {
        _logger = logger;
        _gamesUpdateService = gamesUpdateService;
    }
    
    public async Task Consume(ConsumeContext<ILongGamesUpdateRequestMessage> context)
    {
        await _gamesUpdateService.ProcessAsync(context.Message.Games);
        // TODO: UPDATE RECORD IN TIME;
        // TODO: UPDATE DEVELOPERS ?;
    }
}