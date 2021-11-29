
public class Startup
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddRazorPages();
        serviceCollection.AddServerSideBlazor(x =>
        {
            x.DetailedErrors = true;
        });

        //serviceCollection.AddControllers();
    }

    public void Configure(
        IApplicationBuilder app,
        IConfiguration configuration,
        ILogger<Startup> logger)
    {
        app.UseStaticFiles();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}