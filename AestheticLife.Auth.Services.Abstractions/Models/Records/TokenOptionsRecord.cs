using System.IdentityModel.Tokens.Jwt;

namespace AestheticLife.Auth.Services.Abstractions.Models.Records;

public record TokenOptionsRecord(JwtSecurityToken token, string expiresAt);