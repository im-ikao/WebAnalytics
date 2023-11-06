FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IKao.WebAnalytics/IKao.WebAnalytics.csproj", "IKao.WebAnalytics/"]
RUN dotnet restore "IKao.WebAnalytics/IKao.WebAnalytics.csproj"
COPY . .
WORKDIR "/src/IKao.WebAnalytics"
RUN dotnet build "IKao.WebAnalytics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IKao.WebAnalytics.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IKao.WebAnalytics.dll"]
