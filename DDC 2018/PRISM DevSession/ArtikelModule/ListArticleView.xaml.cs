using System;
using System.Collections.Generic;
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
using DAL;
using Prism.Mvvm;
using Prism.Regions;

namespace ArtikelModule
{
    public class ListArticleViewModel : BindableBase, INavigationAware
    {
        public List<Article> Articles { get; }

        public ListArticleViewModel(IArticleRepository articleRepository)
        {
            Articles = articleRepository.Get();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            MessageBox.Show("Navigate To");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            MessageBox.Show("Navigate From");
        }
    }

    /// <summary>
    /// Interaktionslogik für ListArticleView.xaml
    /// </summary>
    public partial class ListArticleView : UserControl
    {
        public ListArticleView()
        {
            InitializeComponent();
        }
    }
}
