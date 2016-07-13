using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Commands;
using TwitterApi;

namespace DWXPrism.TwitterModule
{
    [Export]
    public partial class TwitterListView : UserControl
    {
        public TwitterListView()
        {
            InitializeComponent();
        }

        [Import]
        internal TwitterListViewModel ViewModel
        {
            get { return DataContext as TwitterListViewModel; }
            set { DataContext = value; }
        }
    }

    [Export]
    public class TwitterListViewModel
    {
        public ICommand LoadTwitterFeed { get; }
        public ObservableCollection<string> Tweets { get; } = new ObservableCollection<string>(); 
        public TwitterListViewModel()
        {
            LoadTwitterFeed = new DelegateCommand(Load);
        }

        private async void Load()
        {
            var dwxTwitterApi = new DwxTwitterApi();
            var result = await dwxTwitterApi.GetDwxTweets("#dwx16");

            foreach(var item in result)
                Tweets.Add(item.Text);

        }
    }
}
