using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Math;
using static System.Console;

namespace DDCSharp
{

    interface ILogger
    {
        void LogMessage(string input);

        void LogMessage()
        {

            var text1 = $@"";
            var text2 = @$"";
            LogMessage("Juhu");
            var input = Console.ReadLine();
        }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName => null;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void PrintPerson()
        {
            Console.WriteLine(FirstName + MiddleName.Length + LastName);
        }
    }

    class MyLogger : ILogger
    {
        public void LogMessage(string input)
        {
            throw new NotImplementedException();
        }
    }

    class Xyz : IAsyncDisposable
    {
        public void Dispose()
        {
        }

        public async IAsyncEnumerable<int> GenerateNumbers()
        {
            await Task.Delay(1000);
            yield return 1;
            await Task.Delay(1000);
            yield return 2;
            await Task.Delay(1000);
            yield return 3;
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(1000);
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteLine(PI);
            await using var instance = new Xyz();
            await foreach (var number in instance.GenerateNumbers())
            {

            }

            int x = 10;
        }


        //using var stream = File.OpenRead("DavidIstSuper.txt");


        //string input = Console.ReadLine();

        //static void Juhu()
        //{
        //    input = null;
        //}

        //int[] numbers = {1, 2, 3, 4};
        //for (int i = 0; i < 10; i++)
        //{
        //    var r = .. + i;
        //    var numbersCopy = "Hallo David"[r];
        //}
        //Console.WriteLine("Hello World!");
    }
}
