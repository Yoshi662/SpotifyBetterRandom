namespace SpotifyBetterRandom.Config;

public record SpotifyClientSettings()
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scopes { get; set; }
    
    public string RedirectUri { get; set; }
    public int RedirectUriPort { get; set; }
}

public record SpotifyEndPoints()
{
    public string BaseURL { get; set; }
    public string TokenEndpoint { get; set; }
    public string AuthorizeEndpoint { get; set; }
}