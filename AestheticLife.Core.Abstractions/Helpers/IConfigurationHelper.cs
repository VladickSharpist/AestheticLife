using Microsoft.Extensions.Configuration;

namespace AestheticLife.Core.Abstractions.Helpers;

public interface IConfigurationHelper
{
    IEnumerable<string> AllowedOrigins { get; }

    string Policy { get; }

    string Localhost { get; }

    IConfigurationSection JwtConfig { get; }
    
    string LocalStoragePath { get; }
    
    string WebStorageAccessLink { get; }
}