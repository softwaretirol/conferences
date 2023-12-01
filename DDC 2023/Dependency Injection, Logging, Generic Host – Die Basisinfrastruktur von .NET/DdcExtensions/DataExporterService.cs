using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class DataExporterService2 : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DataExporterService2(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // var window = _serviceProvider.GetRequiredService<MainWindow>();
        // window.ShowDialog();
        return Task.CompletedTask;
    }
}

public class DataExporterService : IHostedService
{
    private readonly ILogger<DataExporterService> _logger;
    private readonly DataExporterConsoleRunner _console;
    private readonly IHostLifetime _hostLifetime;

    public DataExporterService(ILogger<DataExporterService> logger, DataExporterConsoleRunner console, IHostLifetime hostLifetime)
    {
        _logger = logger;
        _console = console;
        _hostLifetime = hostLifetime;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting DataExporterService {Date}", DateTime.Now);
        await Task.Delay(2000);
        _ = Task.Run(_console.ExportData);
        _logger.LogInformation("Started DataExporterService {Date}", DateTime.Now);
        
        await _hostLifetime.StopAsync(cancellationToken);
        
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
    }
}