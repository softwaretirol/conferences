using System;
using Interfaces;
using Orleans;
using Orleans.Hosting;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts =>
                    parts.AddApplicationPart(typeof(IChatRoom).Assembly).WithReferences().WithCodeGeneration())
                .Build();
            client.Connect().Wait();

            var chatroom = client.GetGrain<IChatRoom>("DemoRoom");
            while (true)
            {
                Console.WriteLine("Ihre Nachricht:");
                chatroom.SendMessage("Hallo Welt").Wait();

                var messages = chatroom.ResolveHistory().Result;
                Console.WriteLine(messages.Length);

                Console.ReadLine();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
