using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            string s1 = "Hallo";
            string s2 = "Hallo";

            fixed (char* pStr = s1)
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    pStr[i] = char.ToUpper(pStr[i]);
                }
            }
            Console.WriteLine(s1);
            Console.WriteLine(s2);

            Console.ReadLine();
            string s3 = "Hallo";

            //AppDomain.CreateDomain() <-- .net core

            //throw new Exception("Ging schief!");
            //Thread.CurrentThread.st
            //int x = 10; //Wo landet des Ding? --> Stack / Heap
            //string str = "Hallo";
            //// Stack <--
            //// Heap  <-- 
            //ABC(x);
            //DEF(); // Tail Optimization Ruyjit (64-Bit JIT Compiler)
            throw new Exception();
            Console.WriteLine();
        }

    }
}
