using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace StatusModule
{
    public class StatusModuleInfo : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionMAnager = containerProvider.Resolve<IRegionManager>();
            regionMAnager.RegisterViewWithRegion(RegionNames.StatusRegion, typeof(StatusBarView));
        }
    }
}
