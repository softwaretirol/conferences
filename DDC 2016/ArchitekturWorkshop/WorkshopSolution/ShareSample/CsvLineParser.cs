using System;

namespace ShareSample
{
    internal class CsvLineParser : ICsvLineParser
    {
        public IShare ParseLine(string line)
        {
            if(line == null)
                throw new ArgumentNullException(nameof(line));

            var lineParts = line.Split(';');
            var shareItem = new Share();
            shareItem.Datum = DateTime.Parse(lineParts[0]);
            double x;
            if (double.TryParse(lineParts[1], out x))
                shareItem.Erster = x;
            if (double.TryParse(lineParts[2], out x))
                shareItem.Hoch = x;
            if (double.TryParse(lineParts[3], out x))
                shareItem.Tief = x;
            if (double.TryParse(lineParts[4], out x))
                shareItem.Schlusskurs = x;
            if (double.TryParse(lineParts[5], out x))
                shareItem.Volumen = x;
            return shareItem;
        }
    }
}