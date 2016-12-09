using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShareSample.Tests
{
    [TestFixture]
    public class CsvLineParserTests
    {
        [Test]
        public void Ensures_GivenValidLine_ResultsInValidIShare()
        {
            var lineParser = new CsvLineParser();
            IShare share = lineParser.ParseLine("04.03.2013;7.654,67;7.708,88;7.637,32;7.691,68;2.148.781.568");
            Assert.That(share.Datum, Is.EqualTo(new DateTime(2013, 3, 4)));
        }
        [Test]
        public void Ensures_GivenNullValueToParse_ExepectArgumentNullException()
        {
            var lineParser = new CsvLineParser();
            Assert.Throws<ArgumentNullException>(() => lineParser.ParseLine(null));
        }
    }
}