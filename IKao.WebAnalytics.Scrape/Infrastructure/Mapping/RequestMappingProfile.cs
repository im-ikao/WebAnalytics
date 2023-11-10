using AutoMapper;
using IKao.WebAnalytics.Domain;
using IKao.WebAnalytics.Domain.DTO;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using IKao.WebAnalytics.Scrape.Domain.Request;
using IKao.WebAnalytics.Scrape.Domain.Response;
using IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;
using NodaTime;

namespace IKao.WebAnalytics.Scrape.Infrastructure.Mapping;

public class RequestMappingProfile : Profile
{
     public RequestMappingProfile()
     {
         var iconConverter = new PrefixToUrlConverter(128, 128);
         var coverConverter = new PrefixToUrlConverter(350, 209);

         CreateMap<GetShortGamesResponse.Item, GameDTO>()
             .ConstructUsing((ctor, ctx) =>
                 new GameDTO(new AppId(ctor.AppId),
                     new Name(ctor.Title),
                     new Description(""),
                     new Description(""),
                     new Description(""),
                     new Developer(ctor.Developer.Id, ctor.Developer.Name),
                     ctor.Features.AgeRating,
                     new Rating(ctor.Rating, ctor.RatingCount),
                     new Counter(ctor.PlayersCount),
                     new Media(new Url(coverConverter.Convert(ctor.Media.Cover.PrefixUrl.ToString(), ctx)),
                         new Url(iconConverter.Convert(ctor.Media.Icon.PrefixUrl.ToString(), ctx))),
                     new Url(""),
                     SystemClock.Instance.GetCurrentInstant(),
                     SystemClock.Instance.GetCurrentInstant(),
                     SystemClock.Instance.GetCurrentInstant(),
                     null,
                     false))
             .ForMember(x => x.Languages, opt => opt.Ignore())
             .ForMember(x => x.Tags, opt => opt.Ignore())
             .ForMember(x => x.Categories, opt => opt.Ignore())
             .ForMember(x => x.Id, opt => opt.Ignore());
         
         CreateMap<GetShortGamesResponse, GameDTO[]>()
             .ConstructUsing((ctor, ctx) =>
                 ctor.GameFeed
                     .Select(x => ctx.Mapper
                         .Map<GameDTO>(x.Items))
                     .ToArray());

         CreateMap<GetShortGamesResponse[], GameDTO[]>()
             .ConstructUsing((ctor, ctx) =>
                 ctor.SelectMany(x => ctx.Mapper.Map<GameDTO[]>(x))
                     .ToArray());
         
         CreateMap<GetLongGamesResponse.Game, GameDTO>()
             .ConstructUsing((ctor, ctx) =>
                 new GameDTO(
                     new AppId(ctor.AppId),
                     new Name(ctor.Title),
                     new Description(ctor.Description),
                     new Description(ctor.SeoDescription),
                     new Description(ctor.Instruction),
                     new Developer(ctor.Developer.Id, ctor.Developer.Name),
                     ctor.Features.AgeRating,
                     new Rating(ctor.Rating, ctor.RatingCount),
                     new Counter(ctor.PlayersCount),
                     new Media(new Url(coverConverter.Convert(ctor.Media.Cover.PrefixUrl.ToString(), ctx)),
                         new Url(iconConverter.Convert(ctor.Media.Icon.PrefixUrl.ToString(), ctx))),
                     new Url(ctor.Url.ToString()),
                     Instant.FromUnixTimeSeconds(ctor.FirstPublished),
                     SystemClock.Instance.GetCurrentInstant(),
                     SystemClock.Instance.GetCurrentInstant(),
                     null,
                     false,
                     ctor.Features.Languages.Select(x => new Language(x)).ToList(),
                     ctor.TagIDs.Select(x => new Marker(x)).ToList(),
                     ctor.CategoryIDs.Select(x => new Marker(x)).ToList()))
             .ForMember(x => x.Languages, opt => opt.Ignore())
             .ForMember(x => x.Tags, opt => opt.Ignore())
             .ForMember(x => x.Categories, opt => opt.Ignore())
             .ForMember(x => x.Id, opt => opt.Ignore());

         CreateMap<GetLongGamesResponse, GameDTO[]>()
             .ConstructUsing((ctor, ctx) =>
                 ctor.Games
                     .Select(x => ctx.Mapper
                         .Map<GameDTO>(x))
                     .ToArray());
         
         CreateMap<GetLongGamesResponse[], GameDTO[]>()
             .ConstructUsing((ctor, ctx) =>
                 ctor.SelectMany(x => ctx.Mapper.Map<GameDTO[]>(x))
                     .ToArray());
         

     }
}