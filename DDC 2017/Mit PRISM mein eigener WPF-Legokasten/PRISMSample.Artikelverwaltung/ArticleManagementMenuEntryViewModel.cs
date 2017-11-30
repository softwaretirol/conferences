using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PRISMSample.Interfaces;
using PRISMSample.Interfaces.Messages;

namespace PRISMSample.Artikelverwaltung
{
    public class ArticleManagementMenuEntryViewModel
    {
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        public ICommand ShowArticleManagementCommand { get; }
        public ICommand SendMessageCommand { get; }
        public ArticleManagementMenuEntryViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            ShowArticleManagementCommand = new DelegateCommand(ShowArticleManagement);
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            eventAggregator.GetEvent<PubSubEvent<IDataItemCreated>>()
                .Publish(new DataItemCreated());
        }

        private void ShowArticleManagement()
        {
            regionManager.RequestNavigate(RegionNames.ContentRegionName, typeof(ArticleListView).FullName);
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegionName, typeof(ArticleListView));
        }
    }

    internal class DataItemCreated : IDataItemCreated
    {
        public string EntityType { get; }
        public int PrimaryKey { get; }
    }
}