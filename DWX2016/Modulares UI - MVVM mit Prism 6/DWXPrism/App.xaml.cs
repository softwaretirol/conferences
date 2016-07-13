using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DWXPrism.TwitterModule;
using LinqToTwitter;
using Prism.Mef;

namespace DWXPrism
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            PrismBootstrapper bootStrapper = new PrismBootstrapper();
            bootStrapper.Run();
        }
    }

    public class PrismBootstrapper : MefBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PrismBootstrapper).Assembly));


            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TwitterModuleInfo).Assembly));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            ((Window)Shell).Show();
        }
    }
}
