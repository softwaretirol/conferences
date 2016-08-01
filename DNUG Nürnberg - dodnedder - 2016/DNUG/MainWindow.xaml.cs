using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DNUG
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Person
            {
                Vorname = "Christian",
                Nachname = "Giesswein"
            };

            var binding = new Binding();
        }
    }

    class MyCustomControl : Control
    {

        public int? Number1
        {
            get { return (int?)GetValue(Number1Property); }
            set { SetValue(Number1Property, value); }
        }

        public static readonly DependencyProperty Number1Property =
            DependencyProperty.Register("Number1", typeof(int?), typeof(MyCustomControl), new PropertyMetadata(null, ));



        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }

        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register("MyText", typeof(string), typeof(MyCustomControl), new PropertyMetadata(string.Empty));



        public MyCustomControl()
        {
            DefaultStyleKey = typeof (MyCustomControl);
        }

    }

    public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SayHello : MarkupExtension
    {
        public SayHello()
        {
            
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return "Hello World";
        }
    }



    public class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }
}
