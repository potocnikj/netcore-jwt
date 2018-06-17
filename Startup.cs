using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using netcore_jwt.Middleware;
using Services;
using Newtonsoft.Json;
using netcore_jwt.DTO;
using System.IO;
using System;

namespace netcore_jwt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            if(this.Configuration.GetValue<bool>("UseMvc"))
            {
                services.AddMvc();
            }
            
            services.AddScoped<JwtService>();
            
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            app.UseMiddleware<ErrorHandlingMiddleware>();

            string pipeline = string.Empty;
            if(this.Configuration.GetValue<bool>("UseMvc"))
            {
                pipeline = "MVC";                
                app.UseMvc();
            }
            else
            {
                pipeline = "JWT Middleware";
                app.UseMiddleware<JwtMiddleware>();                
            }
            
            Console.WriteLine($"Using pipeline: {pipeline}");
        }
    }
}
