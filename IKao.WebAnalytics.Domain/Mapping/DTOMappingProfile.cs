using AutoMapper;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Domain.ValueObjects;
using NodaTime;

namespace IKao.WebAnalytics.Domain.Mapping;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Game, GameDTO>()
            .ConstructUsing(game => new GameDTO(
                game.Id,
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
                dto.Id,
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
        
        CreateMap<Marker, Tag>()
            .ConstructUsing(x => new Tag(x.Id));
        
        CreateMap<Marker, Category>()
            .ConstructUsing(x => new Category(x.Id));

        CreateMap<Marker, Language>()
            .ConstructUsing(x => new Language(x.Id));

        CreateMap<GameDTO, GameStats>()
            .ConstructUsing(x => new GameStats(x.Updated, x.Id, x.Rating, x.Players));

        CreateMap<GameDTO[], GameStats[]>()
            .ConstructUsing((ctor, ctx) => ctor.Select(x => ctx.Mapper.Map<GameStats>(x)).ToArray());

        CreateMap<GameDTO[], Developer[]>()
            .ConstructUsing((ctor, ctx) => ctor.Select(x => x.Developer).ToArray());
        
        CreateMap<GameDTO[], AppId[]>()
            .ConstructUsing((ctor, ctx) => ctor.Select(x => x.Id).ToArray());
        
        CreateMap<GameDTO[], GameCategoryRelation[]>()
            .ConstructUsing((ctor, ctx) => 
                ctor
                .SelectMany(x => x.Categories ?? new List<Marker>())
                .Select(x => ctx.Mapper.Map<GameCategoryRelation>(x))
                .ToArray());
        
        CreateMap<GameDTO[], GameTagRelation[]>()
            .ConstructUsing((ctor, ctx) => 
                ctor
                    .SelectMany(x => x.Tags ?? new List<Marker>())
                    .Select(x => ctx.Mapper.Map<GameTagRelation>(x))
                    .ToArray());
        
        CreateMap<GameDTO[], GameLanguageRelation[]>()
            .ConstructUsing((ctor, ctx) => 
                ctor
                    .SelectMany(x => x.Languages ?? new List<Language>())
                    .Select(x => ctx.Mapper.Map<GameLanguageRelation>(x))
                    .ToArray());
    }
}