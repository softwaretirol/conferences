using Microsoft.Extensions.DependencyInjection;

namespace DdcWorkshop.Main;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMainUI(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();
        services.AddTransient<SampleWindow>();
        services.AddTransient<SampleWindowViewModel>();
        services.AddTransient<IDialogService, DialogService>();
        return services;
    }
}