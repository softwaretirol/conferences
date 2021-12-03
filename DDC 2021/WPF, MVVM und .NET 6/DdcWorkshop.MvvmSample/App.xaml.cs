using System.Windows;
using DdcWorkshop.Data;
using DdcWorkshop.Data.Contracts;
using DdcWorkshop.DependencyInjection;
using DdcWorkshop.Main;
using DdcWorkshop.Status;
using DdcWorkshop.Status.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DdcWorkshop.MvvmSample;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var provider = new ServiceCollection()
            .AddMainUI()
            .AddStatusUI()
            .AddData()
            .BuildServiceProvider();

        ServiceProviderLocator.ServiceProvider = provider;
        var window = provider.GetRequiredService<MainWindow>();
        window.Show();
    }

    private static void ContainerDiy()
    {
        var container = new Container();
        //container.Register<ISessionService, SessionService>();
        container.Register<MainWindowViewModel>();
        container.Register<MainWindow>();

        var window = container.Resolve<MainWindow>();
        window.Show();
    }
}