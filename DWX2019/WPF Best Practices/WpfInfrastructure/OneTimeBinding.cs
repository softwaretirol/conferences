using System.Windows.Data;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation",
    "WpfInfrastructure")]

namespace WpfInfrastructure
{
    public class OneTimeBinding : Binding
    {
        public OneTimeBinding()
        {
            Mode = BindingMode.OneTime;
        }

        public OneTimeBinding(string path) : base(path)
        {
            Mode = BindingMode.OneTime;
        }
    }
}