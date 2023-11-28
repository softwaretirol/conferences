using BlazorUI;
using BlazorUI.Contracts;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddTransient<IPersonDataSource, MyLocalDbPersonDataSource>();
            services.AddWindowsFormsBlazorWebView();
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1
                .RootComponents
                .Add(new RootComponent("#app", typeof(HelloWorld), new Dictionary<string,object>()));
        }
    }
}
