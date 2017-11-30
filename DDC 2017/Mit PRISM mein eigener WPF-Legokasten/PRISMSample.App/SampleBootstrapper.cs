using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PRISMSample.Artikelverwaltung;
using PRISMSample.Bestellverwaltung;
using PRISMSample.Data;
using PRISMSample.Interfaces;

namespace PRISMSample.App
{
    internal class SampleBootstrapper : UnityBootstrapper //Bootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //Container.RegisterType<IArticleProvider, ArticleProvider>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            moduleCatalog.AddModule(typeof(ArtikelverwaltungModule), typeof(DataModule).Name);
            moduleCatalog.AddModule(typeof(BestellverwaltungModule), typeof(DataModule).Name);
            moduleCatalog.AddModule(typeof(DataModule));
        }

        // Shell
        protected override DependencyObject CreateShell()
        {

            // RequestNavigate
            //Container.Resolve<object>("ABC");

            //return new MainWindow(new MainWindowViewModel());
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            var mainWindow = (MainWindow)Shell;
            mainWindow.DataContext = Container.Resolve<MainWindowViewModel>();
            mainWindow.Show();
        }
    }
}