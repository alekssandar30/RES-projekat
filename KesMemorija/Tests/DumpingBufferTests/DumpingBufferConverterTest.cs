using KesMemorija.DumpingBuffer;
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
    public class DumpingBufferConverterTest
    {
        Mock<DumpingBufferConverter> dbcMock = new Mock<DumpingBufferConverter>();

        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_CUSTOM", 1)]
        //[TestCase("CODE_DIGITAL", 0)]
        //[TestCase("CODE_LIMITSET", 1)]
        public void AddCDToDictionaryGoodParameters(string code, int dataset)
        {
            Mock<Dictionary<int, CollectionDescription>> dicMock = new Mock<Dictionary<int, CollectionDescription>>();
            Dictionary<int, CollectionDescription> dicObj = dicMock.Object;
            dicObj.Add(dataset, new CollectionDescription(dataset));
            Mock<Value> valueMock = new Mock<Value>("2212",1111);
            DumpingBufferConverter dbcObj = dbcMock.Object;

            dbcObj.AddCDtoDictionary(code, valueMock.Object, dicObj, dataset);
            Assert.AreEqual(dicObj[dataset].Dpc.dumpingPropertyList[0].DumpingValue, valueMock.Object);
        }


        [Test]
        [TestCase("CODE_ANALOG", 42)]
        [TestCase("CODE_SENSOR",11)]
        [TestCase("", 2)]
        [TestCase("", 0)]
        public void AddCDToDictionaryBadParameters(string code, int dataset)
        {
            Mock<Dictionary<int, CollectionDescription>> dicMock = new Mock<Dictionary<int, CollectionDescription>>();
            Dictionary<int, CollectionDescription> dicObj = dicMock.Object;
            Mock<Value> valueMock = new Mock<Value>("2212", 1111);
            DumpingBufferConverter dbcObj = dbcMock.Object;

            Assert.Throws<ArgumentException>(() =>
            {
                dbcObj.AddCDtoDictionary(code, valueMock.Object, dicObj, dataset);
            });
        }
        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_SENSOR", 4)]
        public void AddCDToDictionaryNullParameters(string code, int dataset)
        {
            DumpingBufferConverter dbcObj = dbcMock.Object;
            Mock<Dictionary<int, CollectionDescription>> dicMock = new Mock<Dictionary<int, CollectionDescription>>();
            Dictionary<int, CollectionDescription> dicObj = dicMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbcObj.AddCDtoDictionary(code, null, dicObj, dataset);
            });
        }

        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_SENSOR", 4)]
        public void AddCDToDictionaryNullParameters2(string code, int dataset)
        {
            DumpingBufferConverter dbcObj = dbcMock.Object;
            Mock<Value> mockValue = new Mock<Value>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbcObj.AddCDtoDictionary(code, mockValue.Object, null, dataset);
            });
        }


    }
}
