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
    public class CollectionDescriptionTest
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void KonstruktorDobriParametri(int dataset)
        {
            CollectionDescription cd = new CollectionDescription(dataset);
            Assert.AreEqual(cd.Dataset, dataset);
        }


        [Test]
        [TestCase(-1)]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(555)]
        [TestCase(-8)]
        public void KonstruktoriLosiParametri(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                CollectionDescription cd = new CollectionDescription(dataset);
            });
        }
    }
}
