using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CSharpSample
{

    public class CsvReader : IDisposable, IAsyncDisposable
    {
        public IEnumerable<Share> ReadFromFile(IEnumerable<string> lines)
        {
            List<Share> shares = new List<Share>();
            foreach (var line in lines.Skip(1))
            {
                var lineParts = line.Split(';');
                var share = new Share()
                {
                    Date = DateTime.Parse(lineParts[0]),
                    Erster = double.TryParse(lineParts[1], out var nr) ? (double?)nr : null
                };
                shares.Add(share);

                if (!IsNull(share.Comment) &&
                    share.Comment!.Length > 30)
                {

                }
            }

            return shares;
        }

        private static bool IsNull(string? s)
        {
            return string.IsNullOrEmpty(s);
        }

        public IEnumerable<(DateTime Date, double? Erster)> ReadFromFileOptimized1(IEnumerable<string> lines)
        {
            //List<(DateTime Date, double? Erster)> shares = new List<(DateTime Date, double? Erster)>();
            foreach (var line in lines.Skip(1))
            {
                var firstIndex = line.IndexOf(';');
                var secondIndex = line.IndexOf(';', firstIndex);
                var dateSpan = line.AsSpan(0, firstIndex);
                ReadOnlySpan<char> ersterSpan = line.AsSpan(firstIndex, secondIndex - firstIndex);

                var erster = double.TryParse(ersterSpan, out var nr) ? (double?)nr : null;
                var date = DateTime.Parse(dateSpan);
                yield return (date, erster);
            }
        }
        public async IAsyncEnumerable<Share> ReadFromFileOptimized2(IEnumerable<string> lines)
        {
            //List<(DateTime Date, double? Erster)> shares = new List<(DateTime Date, double? Erster)>();
            foreach (var line in lines.Skip(1))
            {
                var lineParts = line.Split(';');
                await Task.Delay(100);
                yield return new Share()
                {
                    Date = DateTime.Parse(lineParts[0]),
                    Erster = double.TryParse(lineParts[1], out var nr) ? (double?)nr : null
                };
            }
        }

        public void Dispose()
        {
        }

        public async ValueTask DisposeAsync()
        {
            Dispose();
            await Task.Delay(2000);
        }
    }
}