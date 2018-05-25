using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using netcore_jwt.Middleware;
using Services;

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
            services.AddScoped<JwtService>();
            
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Map("/v1/jwt/encode", appBuilder => {
                
                appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
                appBuilder.Run(async context => {
                        var data = context.Request.Query["data"];
                        var service = context.RequestServices.GetService<JwtService>();
                        var token = service.Encode(data);
                        await context.Response.WriteAsync(token);
                });
            });
             app.Map("/v1/jwt/decode", appBuilder => {
                 
                appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
                appBuilder.Run(async context => {
                        var token = context.Request.Query["token"];
                        var service = context.RequestServices.GetService<JwtService>();
                        var data = service.Decode(token);
                        await context.Response.WriteAsync(data);
                });
            });

            
        }
    }
}
