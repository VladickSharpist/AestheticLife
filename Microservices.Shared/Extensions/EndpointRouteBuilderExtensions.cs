using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace Microservices.Shared.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder ApplyHealthChecks(this IEndpointRouteBuilder builder)
    {
        builder.MapHealthChecks("/api/health", new HealthCheckOptions()    
        {    
            Predicate = _ => true,    
            ResponseWriter = UIResponseWriter.     
                WriteHealthCheckUIResponse
        });
        return builder;
    }
}