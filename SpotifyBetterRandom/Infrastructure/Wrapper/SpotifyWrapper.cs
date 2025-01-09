using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using SpotifyBetterRandom.Config;
using SpotifyBetterRandom.Helpers;
using SpotifyBetterRandom.Infrastructure.Core;
using SpotifyBetterRandom.Model.Requests;
using SpotifyBetterRandom.Model.Responses;

namespace SpotifyBetterRandom.Infrastructure;

public class SpotifyWrapper : ISpotifyWrapper
{
    private readonly IApiCaller _caller;
    private readonly SpotifyClientSettings _settings;
    private readonly SpotifyEndPoints _endPoints;

    public SpotifyWrapper(IApiCaller caller, IConfiguration _config)
    {
        _caller = caller;
        _config.SetConfig(out _settings);
        _config.SetConfig(out _endPoints);
    }

    public string GetToken_ClientCredentials()
    {
        var request = new HttpRequestBuilder(_endPoints.TokenEndpoint)
            .WithMethod(HttpMethod.Post)
            .WithBasicAuth(_settings.ClientId, _settings.ClientSecret)
            .WithContentType("application/x-www-form-urlencoded")
            .Build();
        
        request.Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string?, string?>("grant_type", "client_credentials")
        });
        
        Console.WriteLine(request.Content.ReadAsStringAsync().Result);
        var token = _caller.SendAsync<ClientCredentials_TokenResponse>(request).GetAwaiter().GetResult();
        return token.Value.AccessToken;
    }

    public string GetToken_ImplicitGrant()
    {
        var request = new HttpRequestBuilder(_endPoints.AuthorizeEndpoint)
            .WithMethod(HttpMethod.Get)
            .WithContentType("application/x-www-form-urlencoded")
            .WithQuery("client_id", _settings.ClientId)
            .WithQuery("response_type", "token")
            .WithQuery("redirect_uri", "http://localhost:8888/callback/")
            .WithQuery("scope", _settings.Scopes)
            .Build();
        _caller.SetBaseUri(_endPoints.BaseURL[..^4]); //
        var result = _caller.SendAsync_AsString(request).GetAwaiter().GetResult();
        //_caller.SetBaseUri(_endPoints.BaseURL);
        SimpleHttpServer server = new();
        Task t1 = server.HandleIncomingConnections(result.Value);
        Process.Start("cmd", "/c start http://localhost:8888/callback/");
        t1.Wait();
        return result.Value;
    }
}