using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace StringSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int min = Min(0, 10);

            string input = "42";
            int.TryParse(input, out int r);

            Person person = new VIP();

            if (person is VIP x)
            {
                Console.WriteLine(x);
            }
            else if (person is NotSoVIP y)
            {
                Console.WriteLine(y);
            }

            switch (person)
            {
                case VIP x1:
                    Console.WriteLine(x1);
                    break;
                case NotSoVIP y1:
                    Console.WriteLine(y1);
                    break;
            }

            DoSomething();

            var sb = new StringBuilder();
            for (var i = 0; i < 10000; i++)
            {
                sb.Append("Hallo");
            }
            //string.Format("{1}", "hallo");
            // C# 6.0 - String Interpolation

            string s = $@"SELECT * FROM PERSON {(object) args}
                         WHERE id > 10";

            // C# 6.0 - Null Propagation Operator
            var p = new Person();
            if (p.Child != null && p.Child.Child != null)
            {
                Console.WriteLine(p.Child.Child.ToString());
            }
            Console.WriteLine(p.Child?.Child?.ToString() ?? "Unbekannt");
            Console.WriteLine(sb.ToString());

            //Exception ex = null;
            //try
            //{
            //    await Task.Delay(1000);
            //}
            //catch(Exception e)
            //{
            //    ex = e;
            //}
            //if(ex != null)
            //    await Task.Delay(1000);
            //try
            //{
            //    await Task.Delay(1000);
            //}
            //catch (Exception e) when (e.Message.Length > 100)
            //{
            //    await Task.Delay(1000);
            //}
            //catch (Exception e) 
            //{
            //    await Task.Delay(10000);
            //}

            var result = DoSomething();
        }

        private static (int x1, int y1, List<int> abc) DoSomething()
        {
            int x = 10;
            int y = 20;
            //c#7.0 --> Tuple --> System.ValueTuple (.NET 4.7)
            return (x, y, new List<int>());
        }

        public (string vorname, string nachname) Get()
        {
            return ("","");
        }

        private string wohnort;
        public string Wohnort
        {
            get => wohnort;
            set => wohnort = value;
        }

    }

    class VIP : Person
    {
        private string vorname;
        public VIP() => vorname = "";

        public void Calculate()
        {
            int x1 = 10;
            int y1 = 20;
            int Sum(int x, int y)
            {
                return x + y;
            }

            int result = Sum(x1, y1);
        }
    }

    class Abc
    {
        public void Execute(params int[] numbers)
        {
            
        }
    }

    class NotSoVIP : Person
    {
        
    }
    internal class Person
    {
        public Person Child { get; }// = new Person();
        public string Vorname { get; } = "Christian";
        public string Nachname { get; } = "Giesswein";
        public string FullName => Vorname + Nachname;
        //public string FullName
        //{
        //    get { return Vorname + Nachname; }
        //}
    }
}