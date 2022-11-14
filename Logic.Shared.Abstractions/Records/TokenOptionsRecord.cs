using System.IdentityModel.Tokens.Jwt;

namespace Logic.Shared.Abstractions.Records;

public record TokenOptionsRecord(JwtSecurityToken token, string expiresAt);