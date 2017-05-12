using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETExpert
{
    class MyCollection<T>
        where T : IEnumerable, new()
    {
        public MyCollection()
        {
            string s;
            //new FileInfo();
            T instance = new T(); //Activator.CreateInstance(typeof(T));
            foreach (var x in instance)
            {
                
            }
        }
    }

    class Program
    {

        //ReceiveData()
        //{
        //    while (stream.HasData)
        //    {
        //        Thread.Sleep(10000);
        //        yield return stream.ReadDAta();
        //    }
        //}
        static void Main(string[] args)
        {
            //Bedingung<int> meineBedingung = null;
            var numbers = GenerateNumbers();
            //numbers.Where(x => x > 10)
            //    .Where(x => x < 10)
            //    .GroupBy(x => x % 2 == 0)
            //    .ToDictionary(); ToArray ToList
            var filteredNumbers = numbers.FilterNumbers(number => number % 2 == 0);
            filteredNumbers = numbers.Where(number => number % 2 == 0);
            foreach (var number in filteredNumbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();

            #region NotNeeded
            //if (numbers.Any())
            //{

            //}
            //if (numbers.Count() > 0)
            //{

            //}

            //IEnumerator<int> enumerator = null;
            //try
            //{
            //    enumerator =  numbers.GetEnumerator();
            //    while (enumerator.MoveNext())
            //    {
            //        var x = enumerator.Current;
            //        Console.WriteLine(x);
            //    }
            //}
            //finally
            //{
            //    if (enumerator != null)
            //        enumerator.Dispose();
            //}

            #endregion
        }

        //static bool MeineSuperBedingung(int number)
        //{
        //    return number % 2 == 0;
        //}
        static Random r = new Random((int)DateTime.Now.Ticks);

        static IEnumerable<int> GenerateNumbers() //Iterators - Rückgabewert: IEnumerable / IEnumerable<T>
        {
            //List<int> numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                yield return r.Next(0, 100);
            }
            //return numbers;
        }

     }
    // Action --> immer void
    // Func<T> ---> IMMER einen Rückgabewert
    // delegate bool Bedingung<T>(T instance);

    static class ExtensionMethods
    {
        public static IEnumerable<T> FilterNumbers<T>(this IEnumerable<T> numbers, Func<T, bool> filterBedingung)
        {
            foreach (var item in numbers)
            {
                if (filterBedingung(item))
                    yield return item;
            }
        }
    }
}
