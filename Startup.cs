using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Web.DAL;

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
            var connectionString = _configuration.GetConnectionString("Word_DB");
            services.AddDbContext<WordContext>(options => options.UseSqlServer(connectionString));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
                app
                    .UseDeveloperExceptionPage()
                    .UseStaticFiles()
                    .UseMvc();
        }
    }
}
