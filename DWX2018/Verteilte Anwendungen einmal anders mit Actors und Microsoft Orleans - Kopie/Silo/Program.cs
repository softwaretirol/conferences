using System;
using Implementations;
using Orleans;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            var siloHost = new SiloHostBuilder()
                .Configure<ClusterOptions>(options => { options.ClusterId = options.ServiceId = "DEMO"; })
                .UseLocalhostClustering()
                .AddAdoNetGrainStorageAsDefault(x => x.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=orleans;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ChatRoom).Assembly).WithReferences().WithCodeGeneration())
                .Build();
            Console.WriteLine("Starting...");
            siloHost.StartAsync().Wait();
            Console.WriteLine("Started");
            Console.ReadLine();
        }
    }
}
