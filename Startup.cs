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
            services.AddMvc();
            services.AddScoped<JwtService>();
            
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(Boolean.Parse(Configuration["UseMvc"]))
            {
                Console.WriteLine("using mvc");
                app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseMvc();

            }
            else
            {
                // You could (should?) encapsulate this logic into a piece of middleware. Makes it cleaner and potentialy reusable.
                app.Map("/v1/encode", appBuilder => {
                    
                    appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
                    appBuilder.Run(async context => {
                        var data = context.Request.Query["data"];
                        var service = context.RequestServices.GetService<JwtService>();
                        var token = service.Encode(data);

                        var result = ResponseDTO.Create(token);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    });
                });
                app.Map("/v1/decode", appBuilder => {
                    
                    appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
                    appBuilder.Run(async context => {
                        var token = context.Request.Query["token"];
                        var service = context.RequestServices.GetService<JwtService>();
                        var data = service.Decode(token); 
                            
                        var result = ResponseDTO.Create(data);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    });
                });
            }
        }
    }
}
