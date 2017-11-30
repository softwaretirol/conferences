using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using PRISMSample.Interfaces;

namespace PRISMSample.Artikelverwaltung
{
    public class ArticleListViewModel:INavigationAware,IRegionMemberLifetime
    {
        public ObservableCollection<IArticle> Articles { get;  }

        public ArticleListViewModel(IArticleProvider articleProvider)
        {
            Articles = new ObservableCollection<IArticle>(articleProvider.Get());
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public bool KeepAlive { get; } = false;
    }
}
