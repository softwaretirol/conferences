using System.Windows;
using DDCWPFEF.Data.Queries;
using DDCWPFEF.Data.Queries.Contracts;
using Ninject;

namespace DDCWPFEF.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var kernel = new StandardKernel();
            kernel.Bind<ICustomerQueries>().To<CustomerQueries>();


            var wnd = kernel.Get<MainWindow>();
            wnd.ShowDialog();
        }
    }
}