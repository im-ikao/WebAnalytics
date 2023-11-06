using IKao.WebAnalytics.Domain.DTO;

namespace IKao.WebAnalytics.Domain.Message;

public class IShortGamesUpdateRequestMessage
{
    public GameDTO[] Games { get; }
}