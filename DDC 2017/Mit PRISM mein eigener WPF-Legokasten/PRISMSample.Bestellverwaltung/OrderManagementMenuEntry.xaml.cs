using Prism.Regions;

namespace PRISMSample.Artikelverwaltung
{
    [ViewSortHint("A")]
    public partial class OrderManagementMenuEntry
    {
        public OrderManagementMenuEntry(OrderManagementMenuEntryViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}