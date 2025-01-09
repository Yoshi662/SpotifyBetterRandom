using SpotifyAPI.Web;
using SpotifyBetterRandom.Infrastructure;
using SpotifyBetterRandom.Infrastructure.Contracts;

namespace SpotifyBetterRandom.Services.Implementations;

//Until I have a better way to test everything. I'll use this.
public class TestService
{
    private readonly ISpotifyWrapper _spotifyWrapper;
    private readonly IAuthProvider _authProvider;


    public TestService(ISpotifyWrapper spotifyWrapper, IAuthProvider authProvider)
    {
        _spotifyWrapper = spotifyWrapper;
        _authProvider = authProvider;
        Work();
    }

    private async Task Work()
    {
        var token = await _authProvider.GetAccessTokenAsync();

        var spotifyclient = new SpotifyClient(token);
        spotifyclient.Player.ResumePlayback();
        Task.Delay(3000).Wait();
        spotifyclient.Player.PausePlayback();
    }
}