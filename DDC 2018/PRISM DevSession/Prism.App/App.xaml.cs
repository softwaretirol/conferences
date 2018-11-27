using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ArtikelModule;
using DAL;
using KundenModule;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using StatusModule;

namespace Prism.App
{
    public partial class App : PrismApplication
    {
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<KundenModuleInfo>();
            moduleCatalog.AddModule<ArtikelModuleInfo>();
            moduleCatalog.AddModule<StatusModuleInfo>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IArticleRepository, ArticleRepository>();
        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            return window;
            //return new MainWindow(new MainWindowViewModel());
        }
    }
}
