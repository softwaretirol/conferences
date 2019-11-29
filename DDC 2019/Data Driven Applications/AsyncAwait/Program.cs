using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var current = TaskScheduler.Current;

            Task<int> myTask = WaitSomeTime();
            int result1 = await myTask;
            int result2 = await myTask;
            int result3 = await myTask;
            Console.WriteLine("Hello World!");
        }

        public static async Task<int> WaitSomeTime()
        {
            await Task.Delay(1000);
            await Task.Delay(1000);
            await Task.Delay(1000);
            return 42;
        }
    }
}
