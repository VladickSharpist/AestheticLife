using AestheticLife.Core.Abstractions.Helpers;
using Microsoft.Extensions.Configuration;

namespace AestheticsLife.Core.Helpers;

public class ConfigurationHelper : IConfigurationHelper
{
    private readonly IConfiguration _configuration;

    public ConfigurationHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<string> AllowedOrigins => _configuration
        .GetSection("Origins")
        .GetChildren()
        .Select(ch => ch.Value);

    public string Policy => _configuration
        .GetSection("Policy")
        .Value;

    public string Localhost => _configuration
        .GetSection("Environments")
        .GetSection("Localhost")
        .Value;

    public IConfigurationSection JwtConfig => _configuration
        .GetSection("JwtConfig");

    public string LocalStoragePath => _configuration
        .GetSection("LocalStoragePath")
        .Value;
    
    public string WebStorageAccessLink => _configuration
        .GetSection("WebStorageAccessLink")
        .Value;
}