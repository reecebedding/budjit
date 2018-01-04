using Microsoft.Extensions.DependencyInjection;
using budjit.core.data.SQLite;
using Microsoft.AspNetCore.Hosting;

namespace budjit.ui
{
    public static class DatabaseStartup
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<BudjitContext>();

                dbContext.Database.EnsureCreated();
            }
            return webHost;
        }
    }
}