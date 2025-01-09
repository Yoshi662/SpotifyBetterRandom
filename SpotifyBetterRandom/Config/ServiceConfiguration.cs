using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyBetterRandom.Config;
using SpotifyBetterRandom.Infrastructure;
using SpotifyBetterRandom.Infrastructure.Contracts;
using SpotifyBetterRandom.Infrastructure.Core;
using SpotifyBetterRandom.Infrastructure.Implementations;
using SpotifyBetterRandom.Services.Implementations;

namespace SpotifyBetterRandom;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IApiCaller, ApiCaller>();
        services.AddSingleton<ISpotifyWrapper, SpotifyWrapper>();
        services.AddSingleton<IAuthProvider, AuthProvider>();
        return services;
    }

    public static IServiceCollection AddDebugServices(this IServiceCollection services)
    {
        services.AddSingleton<TestService>();
        return services;
    }
}