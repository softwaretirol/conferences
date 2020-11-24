using System;
using E2E.Api.Hubs;
using E2E.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace E2E.Api
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
            //services.AddTransient<WeatherForecast>();
            //services.AddScoped<WeatherForecast>();
            //services.AddSingleton<WeatherForecast>();


            //services.AddTransient<E2EContext>(sp => new E2EContext());

            services.AddSignalR();
            services.AddSingleton<DataChangedHub>();

            services.AddQueries(Configuration["Db"]);
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "E2E.Api", Version = "v1"}); });

            services.AddOpenApiDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger((Action<SwaggerOptions>)null);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E2E.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DataChangedHub>("/DataChanged");
                endpoints.MapControllers();
            });
        }
    }
}