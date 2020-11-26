using System;

namespace NullableSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // C# 8.0 - Nullable Reference Types
            // - Compilerfeature (Analyse) / Runtime = 0

            // typeof(Program).Assembly.Location.Length

            DoSomething1(null);
            DoSomething2(null);

            

        }

        private static void Juhu(string? x)
        {
            if (IsNull(x))
            {
                return;
            }

            Console.WriteLine(x!.Length);
        }

        private static bool IsNull(object o)
        {
            return o == null;
        }

        public static void DoSomething1(string? input)
        {
            // NRE
            if (input != null && input.Length > 10)
            {

            }
        }
        public static void DoSomething2(string input)
        {
            if (input.Length > 10)
            {

            }
        }
    }
}