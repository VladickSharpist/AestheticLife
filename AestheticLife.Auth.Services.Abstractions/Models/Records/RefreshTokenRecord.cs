namespace AestheticLife.Auth.Services.Abstractions.Models.Records;

public record RefreshTokenRecord(string refreshToken, string expiresAt);