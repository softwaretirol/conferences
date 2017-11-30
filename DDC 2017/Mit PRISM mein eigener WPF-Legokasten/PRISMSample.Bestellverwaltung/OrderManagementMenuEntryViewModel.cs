using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PRISMSample.Bestellverwaltung;
using PRISMSample.Interfaces;
using PRISMSample.Interfaces.Messages;

namespace PRISMSample.Artikelverwaltung
{
    public class OrderManagementMenuEntryViewModel
    {
        private readonly IRegionManager regionManager;
        public ICommand ShowArticleManagementCommand { get; }

        public OrderManagementMenuEntryViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<PubSubEvent<IDataItemCreated>>()
                .Subscribe(OnDataItemCreated, ThreadOption.BackgroundThread);

            this.regionManager = regionManager;
            ShowArticleManagementCommand = new DelegateCommand(ShowArticleManagement);
        }
        private void OnDataItemCreated(IDataItemCreated message)
        {

        }

        private void ShowArticleManagement()
        {
            regionManager.RequestNavigate(RegionNames.ContentRegionName, typeof(OrderListView).FullName);
            //IRegion region = regionManager.Regions[RegionNames.ContentRegionName];
            //region.Activate();
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegionName, typeof(OrderListView));
        }
    }
}