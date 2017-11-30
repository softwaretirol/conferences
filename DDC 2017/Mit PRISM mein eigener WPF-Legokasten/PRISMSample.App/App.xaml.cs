using System.Windows;

namespace PRISMSample.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootStrapper = new SampleBootstrapper();
            bootStrapper.Run();
        }
    }
}