using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using CSharpSample;

namespace Runner
{
    [SimpleJob(RuntimeMoniker.NetCoreApp30)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [MemoryDiagnoser]
    public class MyBenchmarks
    {
        private string[] lines;

        [GlobalSetup]
        public void Setup()
        {
            lines = File.ReadAllLines("Data.csv");
        }

        [Benchmark]
        public void ReadFromFile()
        {
            var reader = new CsvReader();
            var shares = reader.ReadFromFile(lines);
        }
        [Benchmark]
        public void ReadFromFileOptimized1()
        {
            var reader = new CsvReader();
            var shares = reader.ReadFromFileOptimized1(lines);
        }
    }

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            int[] numbers = {1, 2, 3, 4};
            var firstNumbers = numbers.Skip(1).Take(2).ToArray();
            int idx = 1;
            Range r = idx..;
            var firstNumbers2 = numbers[r];


            await using var reader = new CsvReader();

            var shares = reader.ReadFromFileOptimized2(File.ReadAllLines("Data.csv"));
            //BenchmarkRunner.Run<MyBenchmarks>();

            await foreach (var valueTuple in shares)
            {

            }

            int x = 10;

            int largeNumber = 10_000_000;
        }
    }



    //ref struct Y
    //{
    //    private int x;
    //    public WebClient Client { get; private set; }

    //    public Y(int x)
    //    {
    //        this.x = x;
    //        Client = new WebClient();
    //    }
    //}

}