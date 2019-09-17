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
    public class HistoricalTest
    {
        Mock<Historical> historyMock;
        public void SetUp()
        {
            historyMock = new Mock<Historical>();
        }
        #region DatasetAlreadyExist
        [Test]
        [TestCase(2)]
        [TestCase(4)]
        public void DatasetAlreadyExistGoodParameters(int dataset)
        { 
            Assert.False(historyMock.Object.DatasetAlreadyExists(dataset));
        }

        [Test]
        [TestCase(2)]
        [TestCase(4)]
        public void DatasetAlreadyExistGoodParameters2(int dataset)
        {
            Historical historyObj = historyMock.Object;
            historyObj.UpisanDataset[dataset] = true;

            Assert.True(historyMock.Object.DatasetAlreadyExists(dataset));
        }

        [Test]
        [TestCase(-7)]
        [TestCase(12)]
        public void DatasetAlreadyExistBadParameters(int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObject = historyMock.Object;

            Assert.Throws<ArgumentException>(() =>
            {
                historyObject.DatasetAlreadyExists(dataset);
            });
        }
        #endregion

        #region RemoveDataset testovi
        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_SINGLENODE", 2)]
        public void RemoveDatasetGoodParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;

            historyObj.DescriptionDic[dataset].HistoricalList[0].Code = code;
            historyObj.DescriptionDic[dataset].HistoricalList[0].HistoricalValue = new Value("2222", 1000);
            historyObj.DescriptionDic[dataset].HistoricalList[1].Code = code;
            historyObj.DescriptionDic[dataset].HistoricalList[1].HistoricalValue = new Value("2222", 1000);

            Mock<Dictionary<int, Description>> dicMock = new Mock<Dictionary<int, Description>>();
            Dictionary<int, Description> dicObj = dicMock.Object;
            dicObj.Add(dataset, new Description(dataset));
            dicObj[dataset].HistoricalList[0].Code = code;
            dicObj[dataset].HistoricalList[0].HistoricalValue = new Value("2222", 22);

            historyObj.RemoveDataset(dicObj);

            Assert.AreEqual(null, historyObj.DescriptionDic[dataset].HistoricalList[0].Code);
            Assert.AreEqual(null,historyObj.DescriptionDic[dataset].HistoricalList[0].HistoricalValue);
        }
        
        [Test]
        public void RemoveDatasetNullParameters(
)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                historyObj.RemoveDataset(null);
            });
        }
        #endregion

        #region FillHistoryComponent testovi
        [Test]
        public void FillHistoryComponentNullParameters()
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                historyObj.FillHistoryComponent(null);
            });
        }

        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_SINGLENODE", 3)]
        public void FillHistoryComponentGoodParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            Mock<Dictionary<int, Description>> dicMock = new Mock<Dictionary<int, Description>>();
            Dictionary<int, Description> dicObj = dicMock.Object;

            dicObj.Add(dataset, new Description(dataset));
            dicObj[dataset].HistoricalList[0].Code = code;
            dicObj[dataset].HistoricalList[0].HistoricalValue = new Value("2222", 700);
            dicObj[dataset].HistoricalList[0].HistoricalValue.Timestamp = DateTime.Now;
            dicObj[dataset].HistoricalList[1].Code = code;
            dicObj[dataset].HistoricalList[1].HistoricalValue = new Value("2222", 700);
            dicObj[dataset].HistoricalList[1].HistoricalValue.Timestamp = DateTime.Now;

            historyObj.FillHistoryComponent(dicObj);
  
            Assert.AreEqual(historyObj.DescriptionDic[dataset].HistoricalList[0].Code, dicObj[dataset].HistoricalList[0].Code);
        }
        #endregion

        #region ReadFromHistoricalConverter testovi
        [Test]
        public void ReadFromHistoricalConverterNullParameters()
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                historyObj.ReadFromHistoricalConverter(null);
            });
        }

        [Test]
        [TestCase("CODE_DIGITAL", 0)]
        public void ReadFromHistoricalConverterGoodParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            Mock<Dictionary<int, Description>> dicMock = new Mock<Dictionary<int, Description>>();
            Dictionary<int, Description> dicObj = dicMock.Object;
            dicObj.Add(dataset, new Description(dataset));
            dicObj[dataset].Dataset = dataset;
            dicObj[dataset].ID = 5;
            dicObj[dataset].HistoricalList[0].Code = code;
            dicObj[dataset].HistoricalList[0].HistoricalValue = new Value("1337", 500);
            dicObj[dataset].HistoricalList[0].HistoricalValue.Timestamp = DateTime.Now;
            dicObj[dataset].HistoricalList[1].Code = code;
            dicObj[dataset].HistoricalList[1].HistoricalValue = new Value("1337", 500);
            dicObj[dataset].HistoricalList[1].HistoricalValue.Timestamp = DateTime.Now;

            historyObj.ReadFromHistoricalConverter(dicObj);
            Assert.AreEqual(historyObj.DescriptionDic[dataset].HistoricalList[0].Code, dicObj[dataset].HistoricalList[0].Code);
        }
        #endregion

        #region ReadDirectlyFromClient testovi
        [Test]
        [TestCase("CODE_ANALOG", 0)]
        [TestCase("CODE_SENSOR", 4)]
        public void ReadDirectlyFromClientGoodParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            Mock<Value> mockVal = new Mock<Value>("4141", 100);
            Value objVal = mockVal.Object;
            objVal.Timestamp = DateTime.Now;

            historyObj.ReadDirectlyFromClient(code, objVal, dataset);
            Assert.AreEqual(historyObj.DescriptionDic[dataset].HistoricalList[0].HistoricalValue.IDGeoPolozaja, objVal.IDGeoPolozaja);
            Assert.AreEqual(historyObj.DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Potrosnja, objVal.Potrosnja);
            Assert.AreEqual(historyObj.DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Timestamp, objVal.Timestamp);
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("CODE_SENSOR", 7)]
        public void ReadDirectlyFromClientBadParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            Mock<Value> mockVal = new Mock<Value>("4141", 100);
            Value objVal = mockVal.Object;
            objVal.Timestamp = DateTime.Now;

            Assert.Throws<ArgumentException>(() =>
            {
                historyObj.ReadDirectlyFromClient(code, objVal, dataset);
            });
        }

        [TestCase("CODE_DIGITAL", 0)]
        [TestCase("CODE_SENSOR", 4)]
        public void ReadDirectlyFromClientNullParameters(string code, int dataset)
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                historyObj.ReadDirectlyFromClient(code, null, dataset);
            });
        }
        #endregion

        #region CheckDeadbandDifference testovi
        [Test]
        [TestCase("CODE_DIGITAL")]
        [TestCase("CODE_ANALOG")]
        public void CheckDeadbandDifferenceGoodParameters(string code)
        {
            Mock<HistoricalProperty> hp = new Mock<HistoricalProperty>();
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            HistoricalProperty hpObj = hp.Object;
            hpObj.Code = code;
            hpObj.HistoricalValue = new Value("1111", 20000);
            hpObj.HistoricalValue.Timestamp = DateTime.Now;

            Assert.True(historyObj.CheckDeadBand(hpObj));
        }

        [Test]
        [TestCase("CODE_ANALOG")]
        public void CheckDeadbandDifferenceGoodParameters2(string code)
        {
            Mock<HistoricalProperty> hp = new Mock<HistoricalProperty>();
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;
            HistoricalProperty hpObj = hp.Object;
            hpObj.Code = code;
            hpObj.HistoricalValue = new Value("1212", 1);
            hpObj.HistoricalValue.Timestamp = DateTime.Now;

            Assert.False(historyObj.CheckDeadBand(hpObj));
        }

        public void CheckDeadbandDifferenceNullParameters()
        {
            historyMock = new Mock<Historical>();
            Historical historyObj = historyMock.Object;


            Assert.Throws<ArgumentNullException>(() =>
            {
                historyObj.CheckDeadBand(null);
            });
        }
        #endregion

    }
}
