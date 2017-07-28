using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MySample
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            //RuntimeHelpers.PrepareMethod();

            //int x = 10;
            //object o = x;
            //x++;
            Person p = new Person
            {
                Vorname = "Christian",
                Nachname = "Christian",
            };

            var str = "Christian";
            fixed (char* pStr = str)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    pStr[i] = char.ToUpper(pStr[i]);
                }
            }
            Console.WriteLine(str);
            Console.WriteLine(p.Vorname);
            Console.ReadLine();
        }
    }

    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public void SayHello()
        {
            Console.WriteLine("Hello World :-)");
        }
    }
}
