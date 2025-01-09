using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyBetterRandom.Config;
using SpotifyBetterRandom.Helpers;
using SpotifyBetterRandom.Infrastructure.Contracts;

namespace SpotifyBetterRandom.Infrastructure.Implementations;

public class AuthProvider : IAuthProvider
{
    private static EmbedIOAuthServer _server;
    private string _token;
    private readonly SpotifyClientSettings _settings;
    private readonly SpotifyEndPoints _endPoints;
    
    public AuthProvider(IConfiguration config)
    {
        config.SetConfig(out _settings);
        config.SetConfig(out _endPoints);
        
        _token = string.Empty;
        _server = new EmbedIOAuthServer(new Uri(_settings.RedirectUri), _settings.RedirectUriPort);
        
        _server.ImplictGrantReceived += OnImplicitGrantReceived;
        _server.ErrorReceived += OnErrorReceived;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        _token = string.Empty;
        Start().GetAwaiter().GetResult();
        while (string.IsNullOrEmpty(_token))
        {
            await Task.Delay(500);
        }
        return _token;
    }
    
    private async Task Start()
    {
        await _server.Start();
        var request = new LoginRequest(_server.BaseUri, _settings.ClientId, LoginRequest.ResponseType.Token)
        {
            Scope = _settings.Scopes.Split(' ')
        };
        BrowserUtil.Open(request.ToUri());
    }

    private async Task OnImplicitGrantReceived(object sender, ImplictGrantResponse response)
    {
        await _server.Stop();
        _token = response.AccessToken;
    }

    private async Task OnErrorReceived(object sender, string error, string state)
    {
        Console.WriteLine($"Aborting authorization, error received: {error}");
        await _server.Stop();
    }
}