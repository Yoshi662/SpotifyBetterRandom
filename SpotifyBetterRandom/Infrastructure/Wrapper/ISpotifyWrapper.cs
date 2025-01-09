namespace SpotifyBetterRandom.Infrastructure;

public interface ISpotifyWrapper
{
    public string GetToken_ClientCredentials();
    public string GetToken_ImplicitGrant();
}