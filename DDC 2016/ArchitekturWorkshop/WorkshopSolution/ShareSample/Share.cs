using System;

namespace ShareSample
{
    public class Share : IShare
    {
        public DateTime Datum { get; set; }
        public double Erster { get; set; }
        public double Hoch { get; set; }
        public double Tief { get; set; }
        public double Schlusskurs { get; set; }
        public double Volumen { get; set; }

    }
}