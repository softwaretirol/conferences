namespace ShareSample
{
    internal interface ICsvLineParser
    {
        IShare ParseLine(string line);
    }
}