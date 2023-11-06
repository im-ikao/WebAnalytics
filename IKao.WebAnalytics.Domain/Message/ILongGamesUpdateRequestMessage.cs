using AutoMapper;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;

namespace IKao.WebAnalytics.Domain.Message;

public class ILongGamesUpdateRequestMessage
{
    public GameDTO[] Games { get; }

}