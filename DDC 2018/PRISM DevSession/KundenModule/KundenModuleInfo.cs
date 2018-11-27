using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace KundenModule
{
    public class KundenModuleInfo : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<KundenView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, typeof(KundenMenuView));
        }
    }
}
