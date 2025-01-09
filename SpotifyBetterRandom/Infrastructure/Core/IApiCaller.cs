using SpotifyBetterRandom.Infrastructure.Operation;

namespace SpotifyBetterRandom.Infrastructure.Core;

public interface IApiCaller
{
    public Task<Result<TResponse>> SendAsync<TRequest, TResponse>(string endpoint, TRequest data, HttpMethod method);
    public Task<Result<TResponse>> SendAsync<TResponse>(HttpRequestMessage message);
    
    public Task<Result<string>> SendAsync_AsString(HttpRequestMessage message);
    public void SetBaseUri(string uri);
}