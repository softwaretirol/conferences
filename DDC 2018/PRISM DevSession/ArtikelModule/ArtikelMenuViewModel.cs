using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure;
using Infrastructure.Messages;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ArtikelModule
{
    public class ArtikelMenuViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        public ICommand OpenCommand { get; }

        public ArtikelMenuViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            OpenCommand = new DelegateCommand(Open);
        }

        private void Open()
        {
            eventAggregator.GetEvent<PubSubEvent<StatusMessage>>()
                .Publish(new StatusMessage("Juhu: " + DateTime.Now));
            regionManager.RequestNavigate(RegionNames.ContentRegion, "ListArticleView");
        }
    }
}
