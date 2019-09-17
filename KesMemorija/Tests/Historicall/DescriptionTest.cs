using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Historicall
{
    [TestFixture]
    public class DescriptionTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void DescriptionGoodParameters(int dataset)
        {
            Description d = new Description(dataset);
            d.HistoricalList[0].HistoricalValue = new Value();
            d.HistoricalList[1].HistoricalValue = new Value();

            Assert.AreEqual(d.Dataset, dataset);
        }
        [Test]
        [TestCase(-1)]
        [TestCase(1111)]
        public void DescriptionBadParameters2(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Description d = new Description(dataset);

            });
        }

    }
}
