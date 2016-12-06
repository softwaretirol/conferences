using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using DDCSample.Annotations;

namespace DDCSample
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var binding = new Binding();
            //BindingExpression bindingExpression = (BindingExpression)tb.SetBinding(TextBlock.TextProperty, binding);

            //bindingExpression.UpdateTarget();
        }
    }

    public class MyMarkupExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            //target.

            return "Hallo Welt!";
        }
    }


    public class MyControl : Control
    {


        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(MyControl), new PropertyMetadata(0));


    }

    // PropertyDescriptor
    // INotifyPropertyChanged - Reflection - PropertyInfo
    // DependencyProperty
    public class Person : INotifyPropertyChanged
    {
        private int number;
        private int fastNumber = 42;
        private int slowNumber = 1024;
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        //BindingSynchronization <-- .NET 4.5

        public int Number
        {
            get { return number; }
            set
            {
                if (value == number) return;
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public int FastNumber
        {
            get { return fastNumber; }
            set { fastNumber = value; }
        }

        public int SlowNumber
        {
            get
            {
                Thread.Sleep(5000);
                return slowNumber;
            }
            set { slowNumber = value; }
        }

        public Person()
        {
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Number++;
            //    }
            //});
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class PropertyChangedBinding : Binding
    {
        public PropertyChangedBinding()
        {
            this.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        }

        public PropertyChangedBinding(string path) :
            base(path)
        {
            this.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        }
    }
}
