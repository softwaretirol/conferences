using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace NET9.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
#pragma warning disable SYSLIB0011
        BinaryFormatter formatter = new();
        formatter.Serialize(new MemoryStream(), "Hallo");
#pragma warning restore SYSLIB0011
    }
}