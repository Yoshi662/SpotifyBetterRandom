using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SpotifyBetterRandom.Helpers;

public class HttpRequestBuilder : IHttpRequestBuilder
{
    private HttpRequestMessage _request;
    private Dictionary<string,string> _query = new Dictionary<string, string>();

    public HttpRequestBuilder(string endpoint)
    {
        _request = new HttpRequestMessage()
        {
            RequestUri = new Uri(endpoint, UriKind.Relative)
        };
    }
    
    public HttpRequestMessage Build()
    {
        if (_query.Count > 0)
        {
            var query = string.Join("&", _query.Select(x => $"{x.Key}={x.Value}"));
            _request.RequestUri = new Uri($"{_request.RequestUri}?{query}", UriKind.Relative);
        }
        return _request;
    }

    public IHttpRequestBuilder WithHeader(string name, string value)
    {
        _request.Headers.Add(name, value);
        return this;
    }

    public IHttpRequestBuilder WithMethod(HttpMethod method)
    {
        _request.Method = method;
        return this;
    }

    public IHttpRequestBuilder WithBody(string body)
    {
        _request.Content = new StringContent(body, Encoding.UTF8, "application/json");
        return this;
    }

    public IHttpRequestBuilder WithBody<T>(T body)
    {
        var json = JsonSerializer.Serialize(body);
        _request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        return this;
    }
    
    public IHttpRequestBuilder WithQuery(string key, string value)
    {
        _query.Add(key, value);
        return this;
    }

    public IHttpRequestBuilder WithContentType(string contentType)
    {
        if (_request.Content != null)
        {
            _request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        }

        return this;
    }

    public IHttpRequestBuilder WithBearerToken(string token)
    {
        _request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return this;
    }

    public IHttpRequestBuilder WithBasicAuth(string username, string password)
    {
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        _request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        return this;
    }
}