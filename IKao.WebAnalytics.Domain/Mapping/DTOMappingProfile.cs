using AutoMapper;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Domain.Model;

namespace IKao.WebAnalytics.Domain.Mapping;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Game, GameDTO>()
            .ConstructUsing(game => new GameDTO(
                game.Title,
                game.Description,
                game.Seo,
                game.Instruction,
                game.Developer,
                game.Age,
                game.Rating,
                game.Players,
                game.Media,
                game.Play,
                game.Publish,
                game.CreationDate,
                game.ModificationDate,
                game.DeletionDate,
                game.IsDeleted,
                null,
                null,
                null));
        
        CreateMap<GameDTO, Game>()
            .ConstructUsing(dto => new Game(
                dto.Title,
                dto.Description,
                dto.Seo,
                dto.Instruction,
                dto.Developer,
                dto.Age,
                dto.Rating,
                dto.Players,
                dto.Media,
                dto.Play,
                dto.Published,
                dto.Created,
                dto.Updated,
                dto.Deleted,
                dto.IsDeleted));

        CreateMap<ILongGamesUpdateRequestMessage, Game[]>()
            .ForMember(x => x, 
                opt => opt.MapFrom(src => src.Games));

        CreateMap<GameDTO[], ILongGamesUpdateRequestMessage>()
            .ForMember(x => x.Games, 
                opt => opt.MapFrom(src => src));
    }
}