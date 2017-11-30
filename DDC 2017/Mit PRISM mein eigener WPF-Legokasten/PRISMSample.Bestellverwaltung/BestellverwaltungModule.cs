using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PRISMSample.Artikelverwaltung;
using PRISMSample.Interfaces;
using PRISMSample.Interfaces.Messages;

namespace PRISMSample.Bestellverwaltung
{
    public class BestellverwaltungModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public BestellverwaltungModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {

            container.RegisterType<object, OrderListView>(typeof(OrderListView).FullName);
            regionManager.RegisterViewWithRegion(RegionNames.MenuRegionName, typeof(OrderManagementMenuEntry));
        }

    }
}
