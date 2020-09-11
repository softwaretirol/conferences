using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DwxHomeSample
{
    internal class Program
    {
        private static void Main()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\cgiesswein\Desktop\TestData_Large.csv");
            SpanWay(lines);
            OldWay(lines);
        }

        private static void SpanWay(string[] lines)
        {
            var watch = Stopwatch.StartNew();
            var allocatedBefore = AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize;
            List<Share> shares = new List<Share>(lines.Length);
            foreach (var line in lines.Skip(1))
            {
                ReadOnlySpan<char> lineSpan = line.AsSpan();
                var nextIdx = line.IndexOf(';');
                //var datePart = lineSpan.Slice(0, nextIdx);
                var datePart = lineSpan[..nextIdx];

                Share share = new Share();
                share.Datum = DateTime.Parse(datePart);

                var previousIdx = nextIdx + 1;
                nextIdx = line.IndexOf(';', previousIdx);
                //var ersterPart = lineSpan.Slice(previousIdx, nextIdx - previousIdx);
                var ersterPart = lineSpan[previousIdx..nextIdx];
                share.Erster = double.Parse(ersterPart);

                previousIdx = nextIdx + 1;
                nextIdx = line.IndexOf(';', previousIdx);
                var hochPart = lineSpan[previousIdx..nextIdx];
                share.Hoch = double.Parse(hochPart);

                previousIdx = nextIdx + 1;
                nextIdx = line.IndexOf(';', previousIdx);
                var tiefPart = lineSpan[previousIdx..nextIdx];
                share.Tief = double.Parse(tiefPart);

                previousIdx = nextIdx + 1;
                nextIdx = line.IndexOf(';', previousIdx);
                var schlusskursPart = lineSpan[previousIdx..nextIdx];
                share.Schlusskurs = double.Parse(schlusskursPart);

                previousIdx = nextIdx + 1;
                var volumenPart = lineSpan[previousIdx..];
                share.Volumen = double.Parse(volumenPart);

                shares.Add(share);
            }

            var allocatedAfter = AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize;
            var memoryUsage = (allocatedAfter - allocatedBefore) / 1024.0;
            Console.WriteLine(memoryUsage + " kB");
            Console.WriteLine(watch.Elapsed);
            Console.WriteLine(shares.Average(x => x.Schlusskurs));
            Console.WriteLine();
        }

        private static void OldWay(string[] lines)
        {
            var watch = Stopwatch.StartNew();
            var allocatedBefore = AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize;
            List<Share> shares = new List<Share>();
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(';');
                Share share = new Share();
                share.Datum = DateTime.Parse(parts[0]);
                share.Erster = double.Parse(parts[1]);
                share.Hoch = double.Parse(parts[2]);
                share.Tief = double.Parse(parts[3]);
                share.Schlusskurs = double.Parse(parts[4]);
                share.Volumen = double.Parse(parts[5]);
                shares.Add(share);
            }

            var allocatedAfter = AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize;
            var memoryUsage = (allocatedAfter - allocatedBefore) / 1024.0;
            Console.WriteLine(memoryUsage + " kB");
            Console.WriteLine(watch.Elapsed);
            Console.WriteLine(shares.Average(x => x.Schlusskurs));
            Console.WriteLine();
        }
    }

    internal class Share
    {
        public DateTime Datum { get; set; }
        public double Erster { get; set; }
        public double Hoch { get; set; }
        public double Tief { get; set; }
        public double Schlusskurs { get; set; }
        public double Volumen { get; set; }
    }
}