namespace SpotifyBetterRandom.Model.Responses;

public class ClientCredentials_TokenResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
}