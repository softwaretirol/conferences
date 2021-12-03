using DdcWorkshop.Status.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DdcWorkshop.Status;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatusUI(this IServiceCollection services)
    {
        services.AddTransient<IStatusView, StatusView>();
        services.AddSingleton<IStatusViewModel, StatusViewModel>();
        return services;
    }
}