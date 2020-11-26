using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        public void Do(CancellationToken cancellationToken = default)
        {

        }
        static void Main(string[] args)
        {
            Sample();


            int[] numbers = {1, 2, 3, 4, 5};
            int[] result1 = numbers[..];
            int[] result3 = numbers[2..]; // 3,4,5
            int[] result4 = numbers[..2]; // 1 2
            int[] result5 = numbers[2..3]; // 1 2
            int[] result6 = numbers[2..^1]; // 1 2
            int[] result2 = numbers.ToArray();
            string text = "Hallo Welt - heute ist ein sonniger Tag";
            ReadOnlySpan<char> textSpan = text.AsSpan();

            System.Range r = ..; //.netstandard 2.1 --> .NET Core 3.0

            var idx = text.IndexOf(' ');
            ReadOnlySpan<char> firstWord = textSpan[..idx];


            string[] parts = text.Split(' ');


            //var tuple = System.Tuple.Create("asdasd", 23);

            // struct -> Stack      <-- Garbage Collector NICHT überwacht
            // class  -> Heap       <-- GC

            int nr1 = default;
            int nr2 = default(int);

            // System.ValueTuple
            var tupleInstance1 = (Name: "", Arguments: args, Numbers: 1, Counter: 2);
            var tupleInstance2 = (Name: "", Arguments: args, Numbers: 1, Counter: 2);
            if (tupleInstance1 == tupleInstance2)
            {
            }

            _ = IsNumber("1");
            _ = IsNumber("2");
            _ = IsNumber("3");

            static bool IsNumberStatic(string input)
            {
                return int.TryParse(input, out _);
            }
            
            bool IsNumber(string input)
            {
                return int.TryParse(input, out _);
            }
        }



        public interface IX
        {
            void Juhu()
            {
            }
        }

        public class XY : IX
        {
            public void Juhu()
            {
            }
        }
        private static async Task Sample()
        {
            string t = null;
            t ??= $@"{DateTime.Now} \ ";

            await using var ms1 = new MemoryStream();
            await using (var ms2 = new MemoryStream())
            {

            } //-

            await using var x = new X();
            await foreach (var nr in x.GenerateNumbers())
            {
                Console.WriteLine(nr);
            }
        } //-

        class X : IAsyncDisposable
        {
            public async IAsyncEnumerable<int> GenerateNumbers()
            {
                await Task.Delay(100);
                yield return 1;
                await Task.Delay(100);
                yield return 2;
                await Task.Delay(100);
                yield return 3;
                await Task.Delay(100);
                yield return 4;
            }

            public async ValueTask DisposeAsync()
            {
                await Task.Delay(1000);
            }
        }


    }
}
