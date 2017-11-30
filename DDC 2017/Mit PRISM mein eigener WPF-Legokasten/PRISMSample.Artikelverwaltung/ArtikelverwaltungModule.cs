using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using PRISMSample.Interfaces;

namespace PRISMSample.Artikelverwaltung
{
    public class ArtikelverwaltungModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public ArtikelverwaltungModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<object, ArticleListView>(typeof(ArticleListView).FullName);
            regionManager.RegisterViewWithRegion(RegionNames.MenuRegionName, typeof(ArticleManagementMenuEntry));
        }
    }
}
