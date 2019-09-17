using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Writer
{
    [TestFixture]
    public class ReaderTest
    {
        Mock<Reader> mockRead = new Mock<Reader>();

        public void SetUp()
        {
            mockRead = new Mock<Reader>();
        }
        [Test]
        [TestCase("")]
        public void ReadDataBadParameters(string code)
        {
            Reader readObj = mockRead.Object;

            Assert.Throws<ArgumentException>(() =>
            {
                readObj.ReadData(code, DateTime.Now);
            });
        }

        [Test]
        public void ReadDataBadParameters2()
        {
            Reader readObj = mockRead.Object;

            Assert.Throws<ArgumentException>(() =>
            {
                readObj.ReadData(null, DateTime.Now);
            });
        }
    }
}
