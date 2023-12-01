
using DdcExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await new HostBuilder()
    .ConfigureServices(services =>
    {
        // DI Registrierungen
        services.AddSingleton<DataExporterConsoleRunner>();
        services.AddHostedService<DataExporterService>();
        services.AddHostedService<DataExporterService2>();
    })
    .ConfigureLogging(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
    })
    // .ConfigureWindowService(x => x.Name = "asdasd")
    .RunConsoleAsync();



var loggerFactory = LoggerFactory
    .Create(x =>
    {
        x.AddConsole().AddJsonConsole();
        x.AddProvider(new MyLoggerProvider());
    });

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Juhu {Datum} {UserId}", DateTime.Now, 42);



// Microsoft.Extensions.DependencyInjection
// Dependency Injection Container
// 2-Phasen Container
// --> 1. Phase - Registrierung         IServiceCollection        
// --> 2. Phase - Verwendung            IServiceProvider

IServiceCollection services = new ServiceCollection();
services.AddLogging(x =>
{
    x.AddConsole().AddJsonConsole();
    x.AddConfiguration()
    x.AddProvider(new MyLoggerProvider());
});
services.AddSingleton<IDataExporter, DataExporter>(sp => sp.GetRequiredService<DataExporter>());
services.AddSingleton<DataExporter>();
// services.AddSingleton<ILogger>(sp => sp.GetRequiredService<ILogger<Program>>());

services.AddTransient<DataProvider>();
services.AddScoped<DataProviderConfiguration>();
services.AddKeyedTransient<DataProviderConfiguration>("SpecialConfiguration1");
services.AddKeyedTransient<DataProviderConfiguration>("SpecialConfiguration2");

IServiceProvider provider = services.BuildServiceProvider(new ServiceProviderOptions()
{
    ValidateOnBuild = true
}); // Phasenübergang
// Root-Scope (= Singleton)

provider.GetRequiredKeyedService<DataProviderConfiguration>("SpecialConfiguration1");
provider.GetRequiredKeyedService<DataProviderConfiguration>("SpecialConfiguration2");

using (var scope1 = provider.CreateScope())
{
    var configuration1 = scope1.ServiceProvider.GetRequiredService<DataProviderConfiguration>();
    var configuration2 = scope1.ServiceProvider.GetRequiredService<DataProviderConfiguration>();
    Console.WriteLine(configuration1 == configuration2);
}

IDataExporter dataExporter = provider.GetRequiredService<IDataExporter>();
Console.WriteLine(await dataExporter.ExportPersonsToJson());

var dataExporter2 = provider.GetRequiredService<DataExporter>();
Console.WriteLine(await dataExporter2.ExportPersonsToJson());
Console.WriteLine(dataExporter == dataExporter2);
Console.ReadLine();

public class MyLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MyLogger(categoryName);
    }
}

public class MyLogger : ILogger
{
    private readonly string _categoryName;

    public MyLogger(string categoryName)
    {
        _categoryName = categoryName;
    }

    public void Log<TState>(LogLevel logLevel, 
        EventId eventId, 
        TState state,
        Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        Console.WriteLine(message);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}