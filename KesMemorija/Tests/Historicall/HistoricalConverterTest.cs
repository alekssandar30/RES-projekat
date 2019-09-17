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
    public class HistoricalConverterTest
    {
        Mock<HistoricalConverter> hcMock;

        #region ReadFromDumpingBuffer
        [Test]
        public void ReadFromDumpingBufferNullParameters()
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hcObj = hcMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                hcObj.ReadFromDumpingBuffer(null);
            });
        }
        #endregion

        #region CheckDatasetAndCode testovi

        [Test]
        public void CheckDatasetAndCodeNullParameters()
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hcObj = hcMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                hcObj.CheckDatasetAndCode(null);
            });
        }

        [Test]
        [TestCase("CODE_CUSTOM", "CODE_LIMITSET")]
        public void CheckDatasetAndCodeGoodParameters(string code1, string code2)
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hcObj = hcMock.Object;
            Dictionary<int, CollectionDescription> dicobj = new Dictionary<int, CollectionDescription>();
            dicobj.Add(1, new CollectionDescription(1));
            dicobj[1].Dpc.dumpingPropertyList[0].Code = code1;
            dicobj[1].Dpc.dumpingPropertyList[1].Code = code2;
            dicobj[1].Dpc.dumpingPropertyList[0].DumpingValue = new Value("1111", 100);
            dicobj[1].Dpc.dumpingPropertyList[1].DumpingValue = new Value("1111", 100);

            Assert.True(hcObj.CheckDatasetAndCode(dicobj));
        }

        [Test]
        [TestCase("CODE_CUSTOM", "CODE_DIGITAL")]
        public void CheckDatasetAndCodeBadParameters(string code1, string code2)
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hcObj = hcMock.Object;
            Dictionary<int, CollectionDescription> dicobj = new Dictionary<int, CollectionDescription>();
            dicobj.Add(1, new CollectionDescription(1));
            dicobj[1].Dpc.dumpingPropertyList[0].Code = code1;
            dicobj[1].Dpc.dumpingPropertyList[1].Code = code2;
            dicobj[1].Dpc.dumpingPropertyList[0].DumpingValue = new Value("1111", 100);
            dicobj[1].Dpc.dumpingPropertyList[1].DumpingValue = new Value("1111", 100);

            Assert.False(hcObj.CheckDatasetAndCode(dicobj));
        }
        #endregion

        #region DatasetAlreadyExist testovi
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void DatasetAlreadyExistGoodParameters(int dataset)
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;
            Historical history = new Historical();
            history.UpisanDataset[dataset] = true;
            hObj.History = history;

            Assert.True(hObj.DatasetAlreadyExist(dataset));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void DatasetAlreadyExistBadParameters(int dataset)
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;
            Historical history = new Historical();
            history.UpisanDataset[dataset] = false;
            hObj.History = history;

            Assert.False(hObj.DatasetAlreadyExist(dataset));
        }
        #endregion

        #region FillDescription testovi
        [Test]
        public void FillDescriptionNullParameters()
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;
            Dictionary<int, CollectionDescription> dic = new Dictionary<int, CollectionDescription>();
            dic.Add(1, new CollectionDescription(1));


            Assert.Throws<ArgumentNullException>(() =>
            {
                hObj.FillDescription(null, dic);

            });   
        }
        #endregion

        #region CleanDescription testovi
        [Test]
        public void CleanDescriptionGoodReturn()
        {
            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;

            Dictionary<int, Description> dic = new Dictionary<int, Description>();
            dic.Add(1, new Description(1));
            dic[1].HistoricalList[0].Code = "dsa";
            dic[1].HistoricalList[1].Code = "dsa";
            dic[1].HistoricalList[1].HistoricalValue = new Value("1111", 200);
            dic[1].HistoricalList[1].HistoricalValue = new Value("2222", 20);

            hObj.CleanDescription(dic);
            Assert.AreEqual(dic[1].HistoricalList[0].Code, null);
            Assert.AreEqual(dic[1].HistoricalList[1].Code, null);
        }
        #endregion

        #region CheckIfDicIsEmpty testovi
        [Test]
        public void CheckIfDicIsEmptyGoodParameters()
        {
            Dictionary<int, CollectionDescription> dic=new Dictionary<int, CollectionDescription>();
            dic.Add(0, new CollectionDescription(0));
            dic[0].Dpc.dumpingPropertyList[0].Code = "CODE_DIGITAL";
            dic[0].Dpc.dumpingPropertyList[1].Code = "CODE_ANALOG";
            dic[0].Dpc.dumpingPropertyList[0].DumpingValue = new Value("1111", 200);
            dic[0].Dpc.dumpingPropertyList[1].DumpingValue = new Value("1111", 200);

            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;
            
            Assert.False(hObj.CheckIfDicIsEmpty(dic));
        }
        [Test]
        public void CheckIfDicIsEmptyGoodParameters2()
        {
            Dictionary<int, CollectionDescription> dic = new Dictionary<int, CollectionDescription>();
            dic.Add(0, new CollectionDescription(0));
            dic[0].Dpc.dumpingPropertyList[0].Code = null;
            dic[0].Dpc.dumpingPropertyList[1].Code = null;
            dic[0].Dpc.dumpingPropertyList[0].DumpingValue = null;
            dic[0].Dpc.dumpingPropertyList[1].DumpingValue = null;

            hcMock = new Mock<HistoricalConverter>();
            HistoricalConverter hObj = hcMock.Object;

            Assert.True(hObj.CheckIfDicIsEmpty(dic));
        }
        #endregion
    }
}
