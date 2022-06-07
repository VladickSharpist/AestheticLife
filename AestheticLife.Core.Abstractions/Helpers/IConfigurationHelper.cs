namespace AestheticLife.Core.Abstractions.Helpers;

public interface IConfigurationHelper
{
    IEnumerable<string> AllowedOrigins { get; }

    string Policy { get; }

    string Localhost { get; }
}