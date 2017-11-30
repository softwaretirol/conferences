using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using PRISMSample.Interfaces;

namespace PRISMSample.Data
{
    //[ModuleExport]
    public class DataModule : IModule
    {
        private readonly IUnityContainer container;

        public DataModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<IArticleProvider, ArticleProvider>();
        }
    }
}
