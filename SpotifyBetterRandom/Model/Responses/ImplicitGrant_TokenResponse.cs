namespace SpotifyBetterRandom.Model.Responses;

public class ImplicitGrant_TokenResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string State { get; set; }
}