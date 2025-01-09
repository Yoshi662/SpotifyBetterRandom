FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SpotifyBetterRandom/SpotifyBetterRandom.csproj", "SpotifyBetterRandom/"]
RUN dotnet restore "SpotifyBetterRandom/SpotifyBetterRandom.csproj"
COPY . .
WORKDIR "/src/SpotifyBetterRandom"
RUN dotnet build "SpotifyBetterRandom.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SpotifyBetterRandom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotifyBetterRandom.dll"]
