using System.Windows;
using System.Windows.Controls;

namespace DDCWPFEF.Controls
{
    public class LoadingIndicator : ContentControl
    {
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingIndicator),
                new PropertyMetadata(false));

        static LoadingIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingIndicator),
                new FrameworkPropertyMetadata(typeof(LoadingIndicator)));
        }
        // Dependency Properties - propdp


        public bool IsLoading
        {
            get => (bool) GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }
    }
}