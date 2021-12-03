using DdcWorkshop.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DdcWorkshop.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<ISessionService, SessionService>();
        return services;
    }
}