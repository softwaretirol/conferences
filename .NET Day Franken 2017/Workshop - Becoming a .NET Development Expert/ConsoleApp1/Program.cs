using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static int number = 0;
        private static object lockObject = new object();
        static void Main(string[] args)
        {
            Thread t1 = new Thread(DoWork);
            t1.Start();
            Thread t2 = new Thread(DoWork);
            t2.Start();
            Console.ReadLine();
        }

        private static void DoWork()
        {
            while (true)
            {
                lock (lockObject)
                {
                    number = number + 1;
                    Console.WriteLine(number);
                }
            }
        }
    }
}
