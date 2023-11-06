FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IKao.WebAnalytics.Trunk/IKao.WebAnalytics.Trunk.csproj", "IKao.WebAnalytics.Trunk/"]
RUN dotnet restore "IKao.WebAnalytics.Trunk/IKao.WebAnalytics.Trunk.csproj"
COPY . .
WORKDIR "/src/IKao.WebAnalytics.Trunk"
RUN dotnet build "IKao.WebAnalytics.Trunk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IKao.WebAnalytics.Trunk.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IKao.WebAnalytics.Trunk.dll"]
