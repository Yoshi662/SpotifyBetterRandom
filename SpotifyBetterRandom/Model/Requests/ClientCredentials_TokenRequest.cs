using System.Text.Json.Serialization;

namespace SpotifyBetterRandom.Model.Requests;

public class ClientCredentials_TokenRequest
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; } = "client_credentials";
}