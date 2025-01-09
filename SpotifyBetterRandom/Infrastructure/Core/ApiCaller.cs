using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using SpotifyBetterRandom.Config;
using SpotifyBetterRandom.Helpers;
using SpotifyBetterRandom.Infrastructure.Operation;

namespace SpotifyBetterRandom.Infrastructure.Core;

public class ApiCaller : IApiCaller
{
    private readonly SpotifyEndPoints _endPoints;
    private HttpClient _client;

    public ApiCaller(IConfiguration config)
    {
        config.SetConfig(out _endPoints);

        _client = new();
        SetBaseUri(_endPoints.BaseURL);
    }

    public async Task<Result<TResponse>> SendAsync<TRequest, TResponse>(string endpoint, TRequest data,
        HttpMethod method)
    {
        HttpRequestMessage request = new()
        {
            Content = new StringContent(JsonSerializer.Serialize(data)),
            Method = method,
            RequestUri = new Uri(endpoint),
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };

        var httpResponse = await _client.SendAsync(request);
        TResponse responseContent = await httpResponse.Content.ReadFromJsonAsync<TResponse>() ?? default;

        if (!httpResponse.IsSuccessStatusCode)
        {
            return new Result<TResponse>()
            {
                IsSuccess = false,
                StatusCode = httpResponse.StatusCode
            };
        }

        return new Result<TResponse>()
        {
            IsSuccess = true,
            StatusCode = httpResponse.StatusCode,
            Value = responseContent
        };
    }

    public async Task<Result<TResponse>> SendAsync<TResponse>(HttpRequestMessage message)
    {
        var httpResponse = await _client.SendAsync(message);
        Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
        Console.WriteLine(httpResponse.RequestMessage.Content.ReadAsStringAsync().Result);
        if (!httpResponse.IsSuccessStatusCode)
        {
            return new Result<TResponse>()
            {
                IsSuccess = false,
                StatusCode = httpResponse.StatusCode
            };
        }

        TResponse responseContent = await httpResponse.Content.ReadFromJsonAsync<TResponse>() ?? default;
        return new Result<TResponse>()
        {
            IsSuccess = true,
            StatusCode = httpResponse.StatusCode,
            Value = responseContent
        };
    }

    public async Task<Result<string>> SendAsync_AsString(HttpRequestMessage message)
    {
        var httpResponse = await _client.SendAsync(message);
        if (!httpResponse.IsSuccessStatusCode)
        {
            return new Result<string>()
            {
                IsSuccess = false,
                StatusCode = httpResponse.StatusCode
            };
        }

        StreamReader stream = new StreamReader(await httpResponse.Content.ReadAsStreamAsync());
        string responseContent = await stream.ReadToEndAsync() ?? default;
        return new Result<string>()
        {
            IsSuccess = true,
            StatusCode = httpResponse.StatusCode,
            Value = responseContent
        };
    }    
    
    public void SetBaseUri(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            _client.BaseAddress = new Uri(_endPoints.BaseURL, UriKind.Absolute);
        }

        _client.BaseAddress = new Uri(uri, UriKind.Absolute);
    }
}