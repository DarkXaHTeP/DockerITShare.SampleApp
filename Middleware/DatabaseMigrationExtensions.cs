using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampleApp.Web.Middleware
{
    public static class DatabaseMigrationExtensions
    {
        public static IApplicationBuilder EnsureMigrationsApplied<T>(this IApplicationBuilder app) where T:DbContext
        {            
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var logFactory = serviceScope.ServiceProvider.GetService<ILoggerFactory>();
                var log = logFactory.CreateLogger(typeof(DatabaseMigrationExtensions));

                log.LogInformation("Setting 20 seconds timeout for SQL Server image to boot");
                Thread.Sleep(20000);

                log.LogInformation("Applying DB migrations");

                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}