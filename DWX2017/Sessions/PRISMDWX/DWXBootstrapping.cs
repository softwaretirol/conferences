using System.Threading;
using System.Windows;
using DAL;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace PRISMDWX
{
    public class DWXBootstrapping : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Thread splashThread = new Thread(() =>
            {
                SplashScreen splash = Container.Resolve<SplashScreen>();
                splash.ShowDialog();
            });
            splashThread.TrySetApartmentState(ApartmentState.STA);
            splashThread.Start();

            //return new MainWindow();
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var catalog = (ModuleCatalog) ModuleCatalog;
            catalog.AddModule(typeof(DALModule));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            //((Window) Shell).Show();

        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            Application.Current.MainWindow.Show();
        }
    }
}