FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IKao.WebAnalytics.Scrape/IKao.WebAnalytics.Scrape.csproj", "IKao.WebAnalytics.Scrape/"]
RUN dotnet restore "IKao.WebAnalytics.Scrape/IKao.WebAnalytics.Scrape.csproj"
COPY . .
WORKDIR "/src/IKao.WebAnalytics.Scrape"
RUN dotnet build "IKao.WebAnalytics.Scrape.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IKao.WebAnalytics.Scrape.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IKao.WebAnalytics.Scrape.dll"]
