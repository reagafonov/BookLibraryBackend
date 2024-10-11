using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;

namespace WebApi;

// ReSharper disable once ClassNeverInstantiated.Global
public class Program
{
    public static void Main(string[] args)
    {
            CreateHostBuilder(args).Build()
                .MigrateDbContext<DatabaseContext>()
                .Run();
        }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureAppConfiguration((_, _) => { });
            });
}