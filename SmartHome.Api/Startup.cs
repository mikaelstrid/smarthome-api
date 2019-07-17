using JustEat.StatsD;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MikaelStrid.SmartHome.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //services.AddStatsD(
            //    (provider) =>
            //    {
            //        //var options = provider.GetRequiredService<MyOptions>().StatsD;

            //        return new StatsDConfiguration()
            //        {
            //            Host = "127.0.0.1",
            //            Port = 8125,
            //            Prefix = "smarthome",
            //            //OnError = ex => LogError(ex)
            //        };
            //    });
            services.AddStatsD("host.docker.internal", "smarthome");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
