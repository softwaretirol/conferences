using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace DWXWPFBinding
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
        }
    }

    public class MainWindowViewModel
    {
        public ObservableCollection<ImageViewModel> Images { get; } = new ObservableCollection<ImageViewModel>();
        public MainWindowViewModel()
        {
            foreach (var file in Directory.GetFiles(@"C:\Users\cgies\Desktop\Sample", "*.jpg"))
            {
                Images.Add(new ImageViewModel(file));
            }
        }
    }

    public class ImageViewModel
    {
        public ImageViewModel(string file)
        {
            Info = new FileInfo(file);
        }

        public FileInfo Info { get; }

        public ImageSource Image
        {
            get
            {
                return LoadImage(Info.FullName);
            }
        }

        private ImageSource LoadImage(string path)
        {
            var bmp = new BitmapImage();
            using (var stream = File.OpenRead(path))
            {
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = stream;
                bmp.EndInit();
                bmp.Freeze();
            }
            return bmp;
        }

        private static ImageSource fallbackImage;
        public ImageSource FallbackImage
        {
            get { return fallbackImage ?? (fallbackImage = LoadImage(@"C:\Users\cgies\Desktop\Sample\LadeBild.png")); }
        }
    }

    public class Demo
    {
        public int Number { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public Demo()
        {
            Vorname = "Christian";
            Nachname = "Giesswein";
        }
    }


    public class HelloWorld : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return "Hello World";
        }
    }
}
