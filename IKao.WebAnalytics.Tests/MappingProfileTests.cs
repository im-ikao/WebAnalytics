using AutoMapper;
using IKao.WebAnalytics.Domain.Mapping;
using IKao.WebAnalytics.Scrape.Infrastructure.Mapping;

namespace IKao.WebAnalytics.Tests;

public class MappingProfileTests
{
    [Fact]
    public void ValidateMappingConfigurationTest()
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new DTOMappingProfile());
                cfg.AddProfile(new RequestMappingProfile());
            });

        IMapper mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}