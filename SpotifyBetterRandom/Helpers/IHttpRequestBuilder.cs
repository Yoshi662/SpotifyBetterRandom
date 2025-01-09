namespace SpotifyBetterRandom.Helpers;

public interface IHttpRequestBuilder
{
    public HttpRequestMessage Build();
    public IHttpRequestBuilder WithHeader(string name, string value);
    public IHttpRequestBuilder WithMethod(HttpMethod method);
    public IHttpRequestBuilder WithBody(string body);
    public IHttpRequestBuilder WithBody<T>(T body);
    public IHttpRequestBuilder WithQuery(string key, string value);
    public IHttpRequestBuilder WithContentType(string contentType);
    public IHttpRequestBuilder WithBearerToken(string token);
    public IHttpRequestBuilder WithBasicAuth(string username, string password);
}