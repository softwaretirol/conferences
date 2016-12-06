using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageViewer.Annotations;

namespace ImageViewer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private int number;
        public ImageViewModel[] Images { get; set; }

        public int Number
        {
            get { return number; }
            set
            {
                if (value == number) return;
                number = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            Images = Directory.GetFiles(@"C:\Users\cgies\Desktop\Sample", "*.jpg").Select(x => new ImageViewModel(x)).ToArray();

            Task.Run(() =>
            {
                while (true)
                {
                    Number++;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class ImageViewModel
    {
        private static ImageSource loadingImage;
        public string ImagePath { get; }

        public ImageSource ImageData => LoadImage(ImagePath);
        public ImageSource LoadingImage => (loadingImage ?? (loadingImage = LoadImage(@"C:\Users\cgies\Desktop\Sample\LadeBild1.png")));


        private BitmapImage LoadImage(string imagePath)
        {
            var bmp = new BitmapImage();
            using (var file = File.OpenRead(imagePath))
            {
                bmp.BeginInit();
                bmp.StreamSource = file;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.CreateOptions = BitmapCreateOptions.None;
                bmp.EndInit();
                bmp.Freeze();
            }
            return bmp;
        }

        public ImageViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }
    }
}
