using System;
using System.Collections.Generic;
using System.Linq;

namespace DWXCSharp
{
    internal static class MyExtensionMethods
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> numbers, Func<T, bool> bedingung)
        {
            foreach(var nr in numbers)
                if (bedingung(nr))
                    yield return nr;
        } 
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            //"Hello World".IstGerade(x => char.IsUpper(x)).ToList();
            // yield

            var numbers = GenerateNumbers();

            foreach (var filteredNumber in numbers.IstGerade(nr => nr%2 == 0))
            {
                
            }


            Action method = () => Console.WriteLine("Hello World");

            List<Action> actions = new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                int nr = i;
                actions.Add(() => Console.WriteLine(nr));
            }

            foreach (var a in actions)
                a();

            //IEnumerable
            foreach (var x in numbers)
            {
                
            }
            //var enumerator = numbers.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current);
            //}

            Console.ReadLine();
        }

        private static void ShowHelloWorld()
        {
            Console.WriteLine("HEllo World");
        }

        private static IEnumerable<int> GenerateNumbers()
        {
            //List<int> numbers = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                yield return i;
                //numbers.Add(i);
            }
            //return numbers;
        } 
    }
}