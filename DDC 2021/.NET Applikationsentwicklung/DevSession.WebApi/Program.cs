
using DevSession.EfCore.SqlServer;
using DevSession.WebApi.Hubs;
using Microsoft.EntityFrameworkCore;

Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(x =>
    {
        x.UseStartup<Startup>();
    })
    .Build()
    .Run();

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddOpenApiDocument();
        services.AddControllers();
        services.AddSignalR();
        services.AddSingleton<DdcHub>();

        //services.AddDbContext<DdcContextSqlServer>(options =>
        //{
        //    options.UseSqlServer(_configuration["DbConnectionString"]);
        //});
        services.AddPooledDbContextFactory<DdcContextSqlServer>(options =>
        {
            options.UseSqlServer(_configuration["DbConnectionString"]);
        });
    }

    public void Configure(IApplicationBuilder applicationBuilder,
        IDbContextFactory<DdcContextSqlServer> dbFactory)
    {
        using var db = dbFactory.CreateDbContext();
        db.Database.Migrate();

        applicationBuilder.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .Build());

        applicationBuilder.UseRouting(); // "/", "/Conferences"... <= URL
        applicationBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<DdcHub>("/ddchub");
            endpoints.MapControllers();
        });
    }
}
