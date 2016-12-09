using System.IO;

namespace ShareSample
{
    internal class LineProvider : ILineProvider
    {
        private readonly string path;

        public LineProvider()
        {
            path = @"C:\Users\cgies\Source\Repos\conferences\DDC 2016\ArchitekturWorkshop\Workshop\TestData.csv";
        }
        //public LineProvider(string path)
        //{
        //    this.path = path;
        //}

        public string[] ProvideLines()
        {
            // Mocking - MoQ <-- Interfaces / Abstract Class
            // TypeMock / Microsoft Fakes <-- Konkrete Klassen
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
    }
}