using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Web.DAL;
using SampleApp.Web.Extensions;
using SampleApp.Web.Middleware;

namespace SampleApp.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnection = _configuration.GetConnectionString("Word_DB");
            services.AddDbContext<WordContext>(options => options.UseSqlServer(dbConnection));

            var redisConnection = _configuration.GetConnectionString("Word.Redis");
            services.AddDistributedRedisCache(options => { options.Configuration = redisConnection; });
            
            services.AddScoped<IWordRepository, WordRepository>();
            services.Decorate<IWordRepository, CachedWordRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
                app
                    .UseDeveloperExceptionPage()
                    .EnsureMigrationsApplied<WordContext>()
                    .UseStaticFiles()
                    .UseMvc();
        }
    }
}
