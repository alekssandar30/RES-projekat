using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DumpingBufferTests
{
    [TestFixture]
    public class DumpingPropertyTest
    {
        [Test]
        [TestCase("CODE_LIMITSET")]
        [TestCase("CODE_DIGITAL")]
        [TestCase("CODE_SOURCE")]
        [TestCase("CODE_MOTION")]
        public void DumpingPropertyGoodParameters(string code)
        {
            Mock<Value> mockVal = new Mock<Value>();

            DumpingProperty dp = new DumpingProperty(code, mockVal.Object);
            Assert.AreEqual(dp.Code, code);
            Assert.AreEqual(dp.DumpingValue, mockVal.Object);
        }
    }
}
