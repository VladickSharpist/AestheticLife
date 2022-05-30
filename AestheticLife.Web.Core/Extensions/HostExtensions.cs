using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AestheticLife.Web.Core.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDbContext<TContext>(this IHost host)
        where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }

        return host;
    }
}