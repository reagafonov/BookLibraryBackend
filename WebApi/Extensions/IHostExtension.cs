using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDbContext<T>(this IHost host) where T : DbContext
    {
        using var scope = host.Services.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<T>();
        db.Database.Migrate();

        return host;
    }
}