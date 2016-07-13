using System.ComponentModel.Composition;
using DWXPrism.Infrastructure;
using Prism.Logging;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace DWXPrism.TwitterModule
{
    [ModuleExport(typeof(TwitterModuleInfo))]
    public class TwitterModuleInfo : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IRegionManager regionManager;

        [ImportingConstructor]
        public TwitterModuleInfo(ILoggerFacade logger, IRegionManager regionManager)
        {
            this.logger = logger;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof (TwitterListView));
        }
    }
}