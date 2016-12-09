using System.Collections.Generic;
using System.Linq;

namespace ShareSample
{
    internal class ShareProvider : IShareProvider
    {
        private readonly ICsvLineParser lineParser;
        private readonly ILineProvider lineProvider;

        // IoC - Inversion of Control
        public ShareProvider(ILineProvider provider,
            ICsvLineParser lineParser)
        {
            this.lineParser = lineParser;
            lineProvider = provider;
        }

        public IEnumerable<IShare> Provide()
        {
            var shares = new List<IShare>();
            var lines = lineProvider.ProvideLines();
            foreach (var line in SkipHeaderLineOfCsv(lines))
            {
                var shareItem = lineParser.ParseLine(line);
                shares.Add(shareItem);
            }
            return shares;
        }

        private IEnumerable<string> SkipHeaderLineOfCsv(string[] lines)
        {
            return lines.Skip(1);
        }
    }
}