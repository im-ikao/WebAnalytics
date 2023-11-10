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
                null))
            .ForMember(x => x.Languages, opt => opt.Ignore())
            .ForMember(x => x.Tags, opt => opt.Ignore())
            .ForMember(x => x.Categories, opt => opt.Ignore());
        
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
                dto.IsDeleted))
            .ForMember(x => x.Publish, opt => opt.Ignore())
            .ForMember(x => x.CreationDate, opt => opt.Ignore())
            .ForMember(x => x.ModificationDate, opt => opt.Ignore())
            .ForMember(x => x.DeletionDate, opt => opt.Ignore())
            .ForMember(x => x.RelationLanguages, opt => opt.Ignore())
            .ForMember(x => x.RelationCategories, opt => opt.Ignore())
            .ForMember(x => x.RelationTags, opt => opt.Ignore());

        CreateMap<ILongGamesUpdateRequestMessage, Game[]>(MemberList.Destination)
            .ConstructUsing((request, ctx) => request
                .Games
                .Select(dto => ctx.Mapper.Map<Game>(dto))
                .ToArray());

        CreateMap<GameDTO[], ILongGamesUpdateRequestMessage>(MemberList.Destination)
            .ForMember(x => x.Games, 
                opt => opt.MapFrom(src => src));
        
        CreateMap<Marker, Tag>()
            .ConstructUsing(x => new Tag(x.Id))
            .ForMember(x => x.Name, opt => opt.Ignore())
            .ForMember(x => x.RelationGames, opt => opt.Ignore());

        CreateMap<Marker, Category>()
            .ConstructUsing(x => new Category(x.Id))
            .ForMember(x => x.Name, opt => opt.Ignore())
            .ForMember(x => x.RelationGames, opt => opt.Ignore());

        CreateMap<Marker, Language>()
            .ConstructUsing(x => new Language(x.Id))
            .ForMember(x => x.Value, opt => opt.Ignore())
            .ForMember(x => x.RelationGames, opt => opt.Ignore());

        CreateMap<GameDTO, GameStats>()
            .ConstructUsing(x => new GameStats(x.Updated, x.Id, x.Rating, x.Players))
            .ForMember(x => x.CreationDate, opt => opt.Ignore())
            .ForMember(x => x.ModificationDate, opt => opt.Ignore())
            .ForMember(x => x.DeletionDate, opt => opt.Ignore());

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