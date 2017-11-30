using Prism.Regions;

namespace PRISMSample.Artikelverwaltung
{
    [ViewSortHint("B")]
    public partial class ArticleManagementMenuEntry
    {
        public ArticleManagementMenuEntry(ArticleManagementMenuEntryViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}