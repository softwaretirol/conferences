using System;
using Microsoft.Practices.Unity;

namespace ShareSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WorkWithUnity();
            var provider = CreateShareProvider();

            WriteSharesToConsole(provider);
            Console.ReadLine();
        }

        private static void WriteSharesToConsole(IShareProvider provider)
        {
            foreach (var share in provider.Provide())
            {
                Console.WriteLine(share);
            }
        }

        private static IShareProvider CreateShareProvider()
        {
            var instanceProvider = new MySampleDIContainer();
            instanceProvider.RegisterType(typeof(ICsvLineParser), typeof(CsvLineParser));
            instanceProvider.RegisterType(typeof(ILineProvider), typeof(LineProvider));
            instanceProvider.RegisterType(typeof(IShareProvider), typeof(ShareProvider));

            var provider = (IShareProvider) instanceProvider.Resolve(typeof(IShareProvider));

            //// Dependency Injection
            //var lineProvider = new LineProvider(filePath);
            //var lineParser = new CsvLineParser();
            //var provider = new ShareProvider(lineProvider, lineParser);
            return provider;
        }

        private static void WorkWithUnity()
        {
            using (var unityContainer = new UnityContainer())
            {
                unityContainer.RegisterType<ICsvLineParser, CsvLineParser>();
                unityContainer.RegisterType<ILineProvider, LineProvider>(new ContainerControlledLifetimeManager());
                unityContainer.RegisterType<IShareProvider, ShareProvider>();
                var lineProvider1 = unityContainer.Resolve<ILineProvider>();
                var lineProvider2 = unityContainer.Resolve<ILineProvider>();
                var areEqual = ReferenceEquals(lineProvider1, lineProvider2);
                var provider2 = unityContainer.Resolve<IShareProvider>();
            }
        }
    }
}