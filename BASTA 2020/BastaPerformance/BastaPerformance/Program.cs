using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace BastaPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MyBenchmarkSample>();
        }
    }

    class CsvReader
    {
        public IEnumerable<Share> ReadCsv(IEnumerable<string> lines)
        {
            List<Share> shares = new List<Share>();
            foreach (var line in lines.Skip(1))
            {


                int i = 0;

                ReadOnlySpan<char> Read()
                {
                    ReadOnlySpan<char> lineSpan = line;
                    var idx = lineSpan.Slice(i).IndexOf(';');
                    if (idx == -1)
                    {
                        return lineSpan.Slice(i);
                    }
                    var parts0 = lineSpan.Slice(i, idx);
                    i += idx + 1;
                    return parts0;
                }

                var share = new Share();
                share.Datum = DateTime.Parse(Read());
                share.Erster = DoubleParse(Read());
                share.Hoch = DoubleParse(Read());
                share.Tief = DoubleParse(Read());
                share.Schluss = DoubleParse(Read());
                share.Volumen = DoubleParse(Read());
                shares.Add(share);
            }

            return shares;
        }

        private double? DoubleParse(ReadOnlySpan<char> part)
        {
            if (double.TryParse(part, out var result))
                return result;
            return null;
        }
    }

    public class Share
    {
        public DateTime Datum { get; set; }
        public double? Erster { get; set; }
        public double? Hoch { get; set; }
        public double? Tief { get; set; }
        public double? Schluss { get; set; }
        public double? Volumen { get; set; }
    }

    //[SimpleJob(RuntimeMoniker.Net48,1,3,5,100)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31, 1, 3, 5, 100)]
    [MemoryDiagnoser]
    public class MyBenchmarkSample
    {
        //[Benchmark]
        //public List<int> LinqSample()
        //{
        //    var numbers = new int[100_000];
        //    return numbers.Select(x => x).ToList();
        //}

        [Benchmark]
        public List<Share> ReadCsv1()
        {
            var reader = new CsvReader();
            return reader.ReadCsv(File.ReadAllLines(@"c:\users\cgiesswein\Desktop\Data.csv")).ToList();
        }
        [Benchmark]
        public List<Share> ReadCsv2()
        {
            var reader = new CsvReader();
            return reader.ReadCsv(File.ReadLines(@"c:\users\cgiesswein\Desktop\Data.csv")).ToList();
        }
    }
}
