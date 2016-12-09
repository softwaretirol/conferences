using System;

namespace ShareSample
{
    public interface IShare
    {
        DateTime Datum { get; }
        double Erster { get; }
        double Hoch { get; }
        double Tief { get; }
        double Schlusskurs { get; }
        double Volumen { get; }
    }
}