using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Historicall
{
    [TestFixture]
    public class HistoricalPropertyTest
    {
        [Test]
        [TestCase("CODE_ANALOG")]
        [TestCase("CODE_SENSOR")]
        public void HistoricalPropertyGoodParameters(string code)
        {
            Mock<Value> valueMock = new Mock<Value>("2221", 1000);

            HistoricalProperty hp = new HistoricalProperty(code, valueMock.Object);
            Assert.AreEqual(hp.Code, code);
            Assert.AreEqual(hp.HistoricalValue, valueMock.Object);
        }

        [Test]
        public void HistoricalPropertyBadParameters()
        {
            Mock<Value> valueMock = new Mock<Value>("2222", 1000);

            Assert.Throws<ArgumentException>(() =>
            {
                HistoricalProperty hp = new HistoricalProperty("", valueMock.Object);
            });
        }
    }
}
