using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Person
    {
        private string firstName = "Christian";
        private string lastName = "Giesswein";
        public string ToString { get; set; }
        public void SayHello()
        {
            Console.WriteLine("Hello");
        }
    }
    class Program
    {
        [Flags]
        public enum XYZ {A = 0, B = 1, C = 2, D= 4 }

        public static void Test(int x)
        {

        }
        public static void Test(XYZ x)
        {

        }
        static void Main(string[] args)
        {
            Test(XYZ.B);
            Test(1);
            Test(0);
            for (int i = 0; i < 10; i++)
            {
                var p1 = new Person();
            }

            //new string() XmlTextReader
            GC.Collect(2);
            Console.ReadLine();

            Person p;
            //var weakRef = GetWeakPerson();
            GC.Collect(2);
            //if (weakRef.TryGetTarget(out p))
            //{
            //    Console.WriteLine("Lebt");
            //}
            //else
            //{
            //    Console.WriteLine("Tot!");
            //}
            RuntimeHelpers.PrepareMethod(typeof(Person).GetMethod("SayHello").MethodHandle);
            Console.ReadLine();
        }

        //private static WeakReference<Person> GetWeakPerson()
        //{
        //    WeakReference<Person> weakRef = new WeakReference<Person>(new Person());
        //    return weakRef;
        //}
    }
}
