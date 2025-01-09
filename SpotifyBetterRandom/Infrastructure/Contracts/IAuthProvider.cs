namespace SpotifyBetterRandom.Infrastructure.Contracts;

public interface IAuthProvider
{
    public Task<string> GetAccessTokenAsync();
}