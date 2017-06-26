using System.Threading;
using System.Windows;

namespace PRISMDWX
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootStrapper = new DWXBootstrapping();
            bootStrapper.Run();
        }
    }
}