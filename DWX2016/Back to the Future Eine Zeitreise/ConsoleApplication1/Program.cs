using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Davod";

            int? length = name?.Length ?? 0;

            string firstname = "David";
            string lastname = "Tielke";

            string fullname = $"Mein Name ist {firstname?.Length ?? 0} {lastname}";

            Func<int, int, long> del = (z1, z2) => (long)z1 + z2;


        }

        public static void PrintName(string naname2me)
        {
            if (naname2me == null)
            {
              throw new ArgumentNullException(nameof(naname2me));   
            }

        }

        static long Add(int zahl1, int zahl2) => (long) zahl1 + zahl2;
    }
}
