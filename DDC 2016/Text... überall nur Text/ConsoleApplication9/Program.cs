using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {

            string a = "Hallo";
            string b = "hallo";


            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                if (a.ToLower() == b.ToLower())
                {

                }
            }
            watch.Stop();
            Console.WriteLine("ToLower: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                if (a.ToUpper() == b.ToUpper())
                {

                }
            }
            watch.Stop();
            Console.WriteLine("ToUpper: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                //CultureInfo.CurrentCulture
                //Thread.CurrentThread.cult
                if (a.Equals(b, StringComparison.CurrentCultureIgnoreCase))
                {

                }
            }
            string.Concat("Hallo", "Welt", "Sepp");
            watch.Stop();
            Console.WriteLine("Equals: " + watch.Elapsed);


            //string s = "";
            //string s2 = string.Empty;















            Console.WriteLine("Lieber Sepp - Gib dein Passwort ein:");
            string pw = Console.ReadLine();

            Console.ReadLine();





            var totallysecret = "DAS IST ABSOLUT GEHEIM - Geht di nix an";

            var s1 = "hallo";
            var s2 = "hallo";

            bool isEqual = ReferenceEquals(s1, s2);

            var inputText = "hallo";

            //var methods = inputText.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var method = inputText.GetType().GetMethod("FillStringChecked", BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(null, new object[] { inputText, 0, char.ToUpper(inputText[0]).ToString() });
            Console.WriteLine(inputText);

            string myText = "hallo";
            Console.WriteLine(myText);

            //fixed (char* pStr = inputText)
            //{
            //    pStr[0] = char.ToUpper(pStr[0]);
            //}
            //Console.WriteLine(inputText);

            //char firstLetter = char.ToUpper(inputText[0]);
            //string result = firstLetter.ToString();
            //for (int i = 1; i < inputText.Length; i++)
            //{
            //    result += inputText[i];
            //}
            //Console.WriteLine(result);

            //string s = "Ösi";
            //var buffer =Encoding.UTF8.GetBytes(s);
            //Console.WriteLine(buffer.Length);
            Console.ReadLine();
        }
    }
}
