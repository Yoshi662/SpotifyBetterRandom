using Microsoft.Extensions.Configuration;

namespace SpotifyBetterRandom.Helpers;

public static class IConfigurationExtensions
{
    public static void SetConfig<T>(this IConfiguration config, out T? configurable) where T : new()
    {
        configurable = new();
        config.Bind(typeof(T).Name, configurable);
    }
}