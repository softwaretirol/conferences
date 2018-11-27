using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure;
using Prism.Commands;
using Prism.Regions;

namespace KundenModule
{
    public class KundenMenuViewModel
    {
        private readonly IRegionManager regionManager;
        public ICommand OpenCommand { get; }

        public KundenMenuViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            OpenCommand = new DelegateCommand(Open);
        }

        private void Open()
        {
            regionManager.RequestNavigate(RegionNames.ContentRegion, "KundenView");
        }
    }
}
