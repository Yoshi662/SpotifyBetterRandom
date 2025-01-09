using System.Net;

namespace SpotifyBetterRandom.Infrastructure.Operation;

public record struct Result<T>(
    bool IsSuccess,
    T? Value,
    HttpStatusCode StatusCode
);