using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person
            {
                Vorname = "Christian",
                Nachname = "Giesswein;"
            };
            Console.ReadLine();
        }
    }

    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public void SayHello()
        {
            Console.WriteLine("Hallo :-)");
        }
    }
}
